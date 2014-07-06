/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


namespace Client.Logic.Skins
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SdlDotNet.Graphics;
    using SdlDotNet.Widgets;
    using System.Drawing;
    using PMU.Compression.Zip;
    using System.IO;
    using System.Xml;

    class SkinManager
    {
        #region Fields

        static Skin activeSkin;
        static Surface screenBackground;

        #endregion Fields

        #region Properties

        public static Surface ScreenBackground {
            get { return screenBackground; }
            set { screenBackground = value; }
        }

        public static Skin ActiveSkin {
            get { return activeSkin; }
        }

        #endregion Properties

        #region Methods

        public static void ChangeActiveSkin(string skinName) {
            if (activeSkin != null) {
                activeSkin.Unload();
            }
            activeSkin = new Skin();
            activeSkin.LoadSkin(skinName);
            IO.Options.ActiveSkin = skinName;
            if (screenBackground != null) {
                screenBackground.Close();
                screenBackground = null;
            }
            screenBackground = LoadGui("General/Background");
            if (screenBackground != null) {
                //lock (screenBackground) {
                //    screenBackground = screenBackground.CreateStretchedSurface(SdlDotNet.Graphics.Video.Screen.Size);
                //}
            }
            SdlDotNet.Widgets.Widgets.ResourceDirectory = IO.Paths.SkinPath + ActiveSkin.Name + "/Widgets/";
        }

        public static void PlaySkinMusic() {
            //Music.Music.AudioPlayer.PlayMusic("Title.ogg");
            //string activeSkinMusicFile = Music.Music.AudioPlayer.FindMusicFile(Skins.SkinManager.GetActiveSkinFolder() + "Music/", "Title");
            //if (!string.IsNullOrEmpty(activeSkinMusicFile)) {
            //    Music.Music.AudioPlayer.PlayMusicDirect(activeSkinMusicFile, -1, false, true);
            //} else {
            //    Music.Music.AudioPlayer.PlayMusic("Title.ogg");
            //}
            string activeSkinMusicFile = Music.AudioHelper.FindMusicFile(Skins.SkinManager.GetActiveSkinFolder() + "Music/", "Title");
            if (!string.IsNullOrEmpty(activeSkinMusicFile)) {
                Music.Music.AudioPlayer.PlayMusic(activeSkinMusicFile, -1, false, true);
            } else {
                Music.Music.AudioPlayer.PlayMusic("PMD3) Title.ogg");
            }
        }

        public static Surface LoadGui(string guiToLoad) {
            if (IO.IO.FileExists("Skins/" + ActiveSkin.Name + "/" + guiToLoad + "/gui.png")) {
                Surface surf = Logic.Graphics.SurfaceManager.LoadSurface("Skins/" + ActiveSkin.Name + "/" + guiToLoad + "/gui.png");
                Surface surf2 = surf.Convert();
                surf2.Transparent = true;
                surf.Close();
                return surf2;
            }
            return null;
        }

        public static Surface LoadGuiElement(string guiToLoad, string elementName) {
            return LoadGuiElement(guiToLoad, elementName, true);
        }

        public static Surface LoadGuiElement(string guiToLoad, string elementName, bool convert) {
            if (IO.IO.FileExists("Skins/" + ActiveSkin.Name + "/" + guiToLoad + "/" + elementName)) {
                Surface surf = Logic.Graphics.SurfaceManager.LoadSurface("Skins/" + ActiveSkin.Name + "/" + guiToLoad + "/" + elementName);
                if (convert) {
                    Surface surf2 = surf.Convert();
                    surf2.Transparent = true;
                    surf.Close();
                    return surf2;
                } else {
                    return surf;
                }
            }
            return null;
        }

        public static void LoadButtonGui(SdlDotNet.Widgets.Button button) {
            button.BackColor = Color.Transparent;
            button.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            button.BorderStyle = BorderStyle.None;
            button.BackgroundImage = Skins.SkinManager.LoadGuiElement("Game Window", "Widgets/button.png");
            button.HighlightType = HighlightType.Image;
            Surface unstretchedHoverImage = Skins.SkinManager.LoadGuiElement("Game Window", "Widgets/button-h.png");
            button.HighlightSurface = unstretchedHoverImage.CreateStretchedSurface(button.Size);
            unstretchedHoverImage.Close();
        }

        public static void LoadTextBoxGui(SdlDotNet.Widgets.TextBox textBox) {
            textBox.BackColor = Color.Transparent;
            textBox.ForeColor = Color.WhiteSmoke;
            textBox.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackgroundImage = Skins.SkinManager.LoadGuiElement("Game Window", "Widgets/textbox.png");
        }

        public static string GetActiveSkinFolder() {
            return IO.Paths.SkinPath + ActiveSkin.Name + "/";
        }

        public static bool InstallSkin(string skinPackagePath) {
            try {
                using (ZipFile zip = new ZipFile(skinPackagePath)) {
                    bool skinValid = false;
                    foreach (ZipEntry entry in zip.Entries) {
                        if (entry.FileName == "Configuration/config.xml") {
                            using (MemoryStream ms = new MemoryStream()) {
                                entry.Extract(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                skinValid = ValidateSkinConfigEntry(ms);
                            }
                            break;
                        }
                    }
                    if (skinValid) {
                        string skinDir = IO.Paths.SkinPath + Path.GetFileNameWithoutExtension(skinPackagePath);
                        if (Directory.Exists(skinDir) == false) {
                            Directory.CreateDirectory(skinDir);
                        }
                        zip.ExtractAll(skinDir, ExtractExistingFileAction.OverwriteSilently);
                        return true;
                    } else {
                        return false;
                    }
                }
            } catch {
                return false;
            }
        }

        private static bool ValidateSkinConfigEntry(MemoryStream configStream) {
            string creator = null;
            string version = null;
            using (XmlReader reader = XmlReader.Create(configStream)) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Creator": {
                                    creator = reader.ReadString();
                                }
                                break;
                            case "Version": {
                                    version = reader.ReadString();
                                }
                                break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(creator) && !string.IsNullOrEmpty(version)) {
                return true;
            } else {
                return false;
            }
        }

        #endregion Methods
    }
}