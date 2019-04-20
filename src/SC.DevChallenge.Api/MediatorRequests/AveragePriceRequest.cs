﻿using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SC.DevChallenge.Api.Exceptions;
using SC.DevChallenge.Api.Extensions;
using SC.DevChallenge.Api.Models;
using SC.DevChallenge.Core.Services.Contracts;
using SC.DevChallenge.Db.Contexts;
using SC.DevChallenge.Db.Factories.Contracts;
using SC.DevChallenge.Db.Models;
using SC.DevChallenge.Db.Repositories;

namespace SC.DevChallenge.Api.MediatorRequests
{
    public class AveragePriceRequest : IRequest<AveragePriceModel>
    {
        public string Portfolio { get; }
        public string Instrument { get; }
        public string InstrumentOwner { get; }
        public string Date { get; }

        public AveragePriceRequest(string instrument, string instrumentOwner, string portfolio,
            string date)
        {
            Instrument = instrument;
            InstrumentOwner = instrumentOwner;
            Portfolio = portfolio;
            Date = date;
        }
    }

    public class AveragePriceResultHandler : IRequestHandler<AveragePriceRequest, AveragePriceModel>
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly IDateTimeConverter _converter;

        public AveragePriceResultHandler(IDateTimeConverter converter, 
            IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _converter = converter;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<AveragePriceModel> Handle(AveragePriceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var date = request.Date.Parse();

                var timeSlot = _converter.DateTimeToTimeSlot(date);

                var start = _converter.GetTimeSlotStartDate(timeSlot);
                var end = _converter.GetTimeSlotStartDate(timeSlot + 1);

                var isInstrumentOwnerEmpty = string.IsNullOrEmpty(request.InstrumentOwner);
                var isInstrumentEmpty = string.IsNullOrEmpty(request.Instrument);
                var isPortfolioEmpty = string.IsNullOrEmpty(request.Portfolio);

                if (isInstrumentOwnerEmpty && isInstrumentEmpty && isPortfolioEmpty)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest,
                        new
                        {
                            message = "Provide at least one filter"
                        });
                }

                using (var context = _dbContextFactory.CreateContext())
                {
                    var repository = new DbRepository<PriceModel>(context);

                    var priceModels = await repository.FindAsync(x =>
                        (isInstrumentOwnerEmpty || x.InstrumentOwner.Name == request.InstrumentOwner) &&
                        (isInstrumentEmpty || x.Instrument.Name == request.Instrument) &&
                        (isPortfolioEmpty || x.Portfolio.Name == request.Portfolio) &&
                        x.Date >= start && x.Date < end);

                    if (!priceModels.Any())
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound,
                            new
                            {
                                message = "No price models",
                                date = start
                            });
                    }

                    return new AveragePriceModel(start, priceModels.Select(x => x.Price).Average());
                }
            }
            catch (FormatException fex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new
                {
                    message = fex.Message
                });
            }
            catch (ArgumentOutOfRangeException aex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new
                {
                    message = aex.Message,
                    actual = aex.ActualValue
                });
            }
        }
    }
}
