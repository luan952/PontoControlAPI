﻿using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.Marking;

namespace PontoControl.Application.UseCases.Marking.GetByUser
{
    public class GetMarkingsUseCase : IGetMarkingsUseCase
    {
        private readonly IMarkingReadOnlyRepository _markingReadOnlyRepository;
        private readonly IUserLogged _userLogged;

        public GetMarkingsUseCase(IMarkingReadOnlyRepository markingReadOnlyRepository, IUserLogged userLogged)
        {
            _markingReadOnlyRepository = markingReadOnlyRepository;
            _userLogged = userLogged;
        }

        public async Task<List<GetMarkingResponse>> Execute(GetMarkingByUserRequest request)
        {
            var user = await _userLogged.GetUserLogged();
            var listMarkings = await _markingReadOnlyRepository.GetMarkingsByUser(user.Id);

            if (request.StarDate is not null || request.EndDate is not null)
            {
                listMarkings = Filter(listMarkings, request);
            }

            var groupedMarkings = new List<GetMarkingResponse>();
            if (listMarkings.Any())
            {
                groupedMarkings = listMarkings.GroupBy(m => m.Hour.Date)
                                                  .Select(group => new GetMarkingResponse
                                                  {
                                                      Marking = group.Select(g => new MarkingResponse
                                                      {
                                                          Address = g.Address,
                                                          Hour = g.Hour
                                                      }).ToList(),
                                                      Date = group.Key,
                                                      TotalHoursByDay = CalculateTotalHours(group.ToList())
                                                  })
                                                  .ToList();
            }

            return groupedMarkings;
        }

        private static List<Domain.Entities.Marking> Filter(List<Domain.Entities.Marking> markings, GetMarkingByUserRequest filters)
        {
            if (markings.Count == 0)
                return new List<Domain.Entities.Marking>();

            var listFiltered = markings;

            if (filters.StarDate is not null)
                listFiltered = listFiltered.Where(m => m.Hour.Date >= filters.StarDate).ToList();

            if (filters.EndDate is not null)
                listFiltered = listFiltered.Where(m => m.Hour.Date <= filters.EndDate).ToList();

            return listFiltered;
        }

        private static TimeSpan CalculateTotalHours(List<Domain.Entities.Marking> markings)
        {
            TimeSpan morningTime = markings[1].Hour - markings[0].Hour;
            TimeSpan affternoonTime = markings[3].Hour - markings[2].Hour;
            
            var totalHours = morningTime + affternoonTime;

            return totalHours;
        }
    }
}
