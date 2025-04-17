using MeterRead.Application.Interfaces;
using MeterRead.Application.Models;
using MeterRead.Data.Interfaces;

namespace MeterRead.Application.Services;

public class MeterDataValidationService(IUnitOfWork unitOfWork) : IMeterValidationService
{
    public (MeterReadings validReadings, MeterReadings invalidReadings) ValidateReadings(MeterReadings readings)
    {
        var invalidReadings = new MeterReadings();
        var validReadings = new MeterReadings();

        var duplicateReadings = GetDuplicateReadings(readings);
        var invalidAccounts = GetInvalidAccounts(readings);

        invalidReadings.AddRange(duplicateReadings);
        invalidReadings.AddRange(invalidAccounts);

        validReadings.AddRange(readings.Where(x => invalidReadings.All(y => y.ReadingId != x.ReadingId)).ToList());

        return (validReadings, invalidReadings);
    }

    private List<InvalidMeterReading> GetInvalidAccounts(List<MeterReading> readings)
    {
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
        return invalidAccounts;
    }

    private List<InvalidMeterReading> GetDuplicateReadings(List<MeterReading> readings)
    {
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
        return duplicateReadings;
    }
}