using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Application.Features.JournalEntries
{
    public class GetJournalEntriesHandler : IRequestHandler<GetJournalEntriesQuery, IEnumerable<JournalEntryDto>>
    {
        private readonly IJournalEntryRepository _repository;
        private readonly IMapper _mapper;

        public GetJournalEntriesHandler(IJournalEntryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JournalEntryDto>> Handle(GetJournalEntriesQuery request, CancellationToken cancellationToken)
        {
            var entries = await _repository.GetAllAsync();
            var result = new List<JournalEntryDto>();

            foreach (var entry in entries)
            {
                var dto = _mapper.Map<JournalEntryDto>(entry);
                var lines = await _repository.GetLinesByEntryIdAsync(entry.JournalEntryId);
                dto.Lines = lines.Select(x => _mapper.Map<JournalEntryLineDto>(x)).ToList();
                result.Add(dto);
            }

            return result;
        }




    }
}