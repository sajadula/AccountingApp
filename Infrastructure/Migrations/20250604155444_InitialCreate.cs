using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- EF‐generated: CreateAccounts, CreateJournalEntries, CreateJournalEntryLines tables ---
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    JournalEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.JournalEntryId);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLines",
                columns: table => new
                {
                    JournalEntryLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalEntryId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLines", x => x.JournalEntryLineId);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_JournalEntryId",
                table: "JournalEntryLines",
                column: "JournalEntryId");

            // --- Begin adding stored procedures ---

            // sp_CreateAccount
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_CreateAccount
            @Name NVARCHAR(100),
            @Type NVARCHAR(50),
            @Id INT OUTPUT
        AS
        BEGIN
            INSERT INTO Accounts (Name, Type)
            VALUES (@Name, @Type);
            SET @Id = SCOPE_IDENTITY();
        END
    ");

            // sp_GetAccounts
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_GetAccounts
        AS
        BEGIN
            SELECT AccountId, Name, Type FROM Accounts;
        END
    ");

            // sp_CreateJournalEntry
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_CreateJournalEntry
            @Date DATETIME,
            @Description NVARCHAR(250),
            @Id INT OUTPUT
        AS
        BEGIN
            INSERT INTO JournalEntries (Date, Description)
            VALUES (@Date, @Description);
            SET @Id = SCOPE_IDENTITY();
        END
    ");

            // sp_CreateJournalEntryLine
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_CreateJournalEntryLine
            @JournalEntryId INT,
            @AccountId INT,
            @Debit DECIMAL(18,2),
            @Credit DECIMAL(18,2)
        AS
        BEGIN
            INSERT INTO JournalEntryLines (JournalEntryId, AccountId, Debit, Credit)
            VALUES (@JournalEntryId, @AccountId, @Debit, @Credit);
        END
    ");

            // sp_GetJournalEntries
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_GetJournalEntries
        AS
        BEGIN
            SELECT JournalEntryId, Date, Description FROM JournalEntries;
        END
    ");

            // sp_GetJournalEntryLines
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_GetJournalEntryLines
            @JournalEntryId INT
        AS
        BEGIN
            SELECT JournalEntryLineId, JournalEntryId, AccountId, Debit, Credit
            FROM JournalEntryLines
            WHERE JournalEntryId = @JournalEntryId;
        END
    ");

            // sp_GetTrialBalance
            migrationBuilder.Sql(@"
        CREATE PROCEDURE sp_GetTrialBalance
        AS
        BEGIN
            SELECT 
                a.Name    AS AccountName,
                a.Type    AS AccountType,
                SUM(l.Debit - l.Credit) AS NetBalance
            FROM Accounts a
            INNER JOIN JournalEntryLines l ON a.AccountId = l.AccountId
            GROUP BY a.Name, a.Type;
        END
    ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // --- Drop stored procedures in reverse order ---
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetTrialBalance");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetJournalEntryLines");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetJournalEntries");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_CreateJournalEntryLine");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_CreateJournalEntry");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAccounts");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_CreateAccount");

            // --- Then drop tables ---
            migrationBuilder.DropTable(name: "JournalEntryLines");
            migrationBuilder.DropTable(name: "JournalEntries");
            migrationBuilder.DropTable(name: "Accounts");
        }

    }
}
