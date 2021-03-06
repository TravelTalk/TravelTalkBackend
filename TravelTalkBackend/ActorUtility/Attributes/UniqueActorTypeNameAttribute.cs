﻿namespace ActorUtilityTravelTalk.ActorUtility.Attributes {
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class UniqueActorTypeNameAttribute : Attribute {
        public readonly string UniqueName;

        public UniqueActorTypeNameAttribute(string uniqueName) {
            UniqueName = uniqueName;
        }
    }
}
