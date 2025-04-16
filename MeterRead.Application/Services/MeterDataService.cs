using MeterRead.Application.Interfaces;
using MeterRead.Application.Models;
using MeterRead.Data;

namespace MeterRead.Application.Services
{
    public class MeterDataService(IUnitOfWork unitOfWork) : IMeterDataService
    {
        public IEnumerable<InvalidMeterReading> ValidateReadings(List<MeterReading> readings)
        {
            var invalidReadings = new List<InvalidMeterReading>();

            var duplicateReadings = (from reading in readings 
                let existingReading = unitOfWork.ReadingRepository.Get(r => r.AccountId == reading.AccountId) 
                let isDuplicate = existingReading.Any(r => DateTime.Parse(r.DateTime) >= reading.MeterReadingDate && r.Value == reading.MeterReadingValue) 
                where isDuplicate 
                select new InvalidMeterReading
                {
                    ReadingId = reading.ReadingId,
                    AccountId = reading.AccountId, 
                    MeterReadingDate = reading.MeterReadingDate,
                    MeterReadingValue = reading.MeterReadingValue, 
                    Reason = "Duplicate reading"
                }).ToList();

            var invalidAccounts = (from reading in readings 
                let account = unitOfWork.AccountRepository.Get(a => a.AccountId == reading.AccountId).FirstOrDefault() 
                where account is null 
                select new InvalidMeterReading
                {
                    ReadingId = reading.ReadingId,
                    AccountId = reading.AccountId,
                    MeterReadingDate = reading.MeterReadingDate,
                    MeterReadingValue = reading.MeterReadingValue,
                    Reason = "Account does not exist"
                }).ToList();

            invalidReadings.AddRange(duplicateReadings);
            invalidReadings.AddRange(invalidAccounts);

            foreach (var reading in readings.Where(r => invalidReadings.All(i => i.AccountId != r.AccountId)))
            {
                var invalidReading = new InvalidMeterReading
                {
                    ReadingId = reading.ReadingId,
                    AccountId = reading.AccountId,
                    MeterReadingDate = reading.MeterReadingDate,
                    MeterReadingValue = reading.MeterReadingValue,
                    Reason = string.Empty
                };

                if (reading.AccountId <= 0)
                {
                    invalidReadings.Add(invalidReading with
                    {
                        Reason = "AccountId must be greater than 0"
                    });
                }

                if (reading.MeterReadingDate == default)
                {
                    invalidReadings.Add(invalidReading with
                    {
                        Reason = "MeterReadingDate cannot be empty"
                    });
                }
                
                if (reading.MeterReadingDate > DateTime.Now)
                {
                    invalidReadings.Add(invalidReading with
                    {
                        Reason = "MeterReadingDate cannot be in the future"
                    });
                }

                if (string.IsNullOrEmpty(reading.MeterReadingValue))
                {
                    invalidReadings.Add(invalidReading with 
                    {
                        Reason = "MeterReadValue cannot be empty"
                    });
                }
            }

            return invalidReadings;
        }

        public async Task ImportReadings(IEnumerable<MeterReading> readings)
        {
            var readingEntities = readings.Select(r => new Reading
            {
                AccountId = r.AccountId,
                DateTime = r.MeterReadingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Value = r.MeterReadingValue
            });

            //Insert readings
            foreach (var reading in readingEntities) await unitOfWork.ReadingRepository.InsertAsync(reading);

            //Save changes
            await unitOfWork.SaveChangesAsync();
        }
    }
}
