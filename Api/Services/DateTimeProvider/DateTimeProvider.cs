﻿namespace WebAppAuthorization.Services.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() => DateTime.Now;
}