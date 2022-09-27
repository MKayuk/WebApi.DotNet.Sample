﻿using MediatR;

namespace WebApi.DotNet.Sample.Helpers.Notification
{
    public class Notification : INotification
    {
        public Notification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }
    }
}
