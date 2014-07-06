using System;
using PMU.Core;
namespace Client.Logic.Stories
{
    interface ISegment
    {
        Client.Logic.Enums.StoryAction Action { get; }
        ListPair<string, string> Parameters { get; }
        bool UsesSpeechMenu { get; }

        void Process(StoryState state);
        void LoadFromSegmentData(ListPair<string, string> parameters);
    }
}
