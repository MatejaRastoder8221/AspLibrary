﻿namespace Library.Api.Extenstions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   !string.IsNullOrWhiteSpace(value);
        }
    }
}
