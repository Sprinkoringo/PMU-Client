using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Client.Logic.Music
{
    class TrackList
    {
        List<TrackListEntry> entries;

        public List<TrackListEntry> Entries {
            get { return entries; }
        }

        public TrackList() {
            entries = new List<TrackListEntry>();
        }

        public void Load(XmlReader reader) {
            while (reader.Read()) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "Entry": {
                                using (XmlReader subReader = reader.ReadSubtree()) {
                                    entries.Add(LoadEntry(subReader));
                                }
                            }
                            break;
                    }
                }
            }
        }

        private TrackListEntry LoadEntry(XmlReader reader) {
            TrackListEntry entry = new TrackListEntry();
            while (reader.Read()) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "TrackName": {
                                entry.TrackName = reader.ReadString();
                            }
                            break;
                    }
                }
            }
            return entry;
        }
    }
}
