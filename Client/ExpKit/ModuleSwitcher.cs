using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.ExpKit
{
    class ModuleSwitcher
    {
        List<IKitModule> allKitModules;
        List<IKitModule> availableKitModules;

        public List<IKitModule> AllKitModules {
            get {
                return allKitModules;
            }
        }

        public List<IKitModule> AvailableKitModules {
            get { return availableKitModules; }
        }

        public ModuleSwitcher() {
            allKitModules = new List<IKitModule>();
            availableKitModules = new List<IKitModule>();
            LoadKitModules();
            CreateAvailableModulesList();
        }

        public void LoadKitModules() {
            ExpKit.Modules.kitDebug kitDebug = new ExpKit.Modules.kitDebug("kitDebug");
            kitDebug.Created(allKitModules.Count);
            kitDebug.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitDebug);

            ExpKit.Modules.kitChat kitChat = new ExpKit.Modules.kitChat("kitChat");
            kitChat.Created(allKitModules.Count);
            kitChat.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitChat);

            ExpKit.Modules.kitCounter kitCounter = new ExpKit.Modules.kitCounter("kitCounter");
            kitCounter.Created(allKitModules.Count);
            kitCounter.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitCounter);

            ExpKit.Modules.kitParty kitParty = new ExpKit.Modules.kitParty("kitParty");
            kitParty.Created(allKitModules.Count);
            kitParty.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitParty);

            ExpKit.Modules.kitFriendsList kitFriendsList = new ExpKit.Modules.kitFriendsList("kitFriendsList");
            kitFriendsList.Created(allKitModules.Count);
            kitFriendsList.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitFriendsList);

            ExpKit.Modules.kitMapReport kitMapReport = new ExpKit.Modules.kitMapReport("kitMapReport");
            kitMapReport.Created(allKitModules.Count);
            kitMapReport.EnabledChanged += new EventHandler(module_EnabledChanged);
            allKitModules.Add(kitMapReport);
        }

        void module_EnabledChanged(object sender, EventArgs e) {
            CreateAvailableModulesList();
        }

        void CreateAvailableModulesList() {
            availableKitModules.Clear();
            for (int i = 0; i < allKitModules.Count; i++) {
                if (allKitModules[i].Enabled) {
                    availableKitModules.Add(allKitModules[i]);
                } else {
#if DEBUG
                    //if (allKitModules[i].ModuleID == Enums.ExpKitModules.Debug) {
                    //    availableKitModules.Add(allKitModules[i]);
                    //}
#endif
                }
            }
        }

        public void DisableAllModules() {
            for (int i = 0; i < allKitModules.Count; i++) {
                allKitModules[i].EnabledChanged -= new EventHandler(module_EnabledChanged);
                allKitModules[i].Enabled = false;
                allKitModules[i].EnabledChanged += new EventHandler(module_EnabledChanged);
            }
            availableKitModules.Clear();
        }

        public IKitModule GetAvailableKitModule(int index) {
            if (index > availableKitModules.Count - 1) {
                return availableKitModules[availableKitModules.Count - 1];
            } else {
                return availableKitModules[index];
            }
        }

        public IKitModule FindAvailableKitModule(Enums.ExpKitModules module) {
            for (int i = 0; i < availableKitModules.Count; i++) {
                if (availableKitModules[i].ModuleID == module) {
                    return availableKitModules[i];
                }
            }
            return null;
        }

        public bool IsModuleAvailable(Enums.ExpKitModules module) {
            for (int i = 0; i < availableKitModules.Count; i++) {
                if (availableKitModules[i].ModuleID == module) {
                    return true;
                }
            }
            return false;
        }

        public IKitModule FindKitModule(Enums.ExpKitModules module) {
            for (int i = 0; i < allKitModules.Count; i++) {
                if (allKitModules[i].ModuleID == module) {
                    return allKitModules[i];
                }
            }
            return null;
        }
    }
}
