using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Graphics;
using Client.Logic.Missions;

namespace Client.Logic.Widgets
{
    class MissionTitle : Panel
    {
        Label lblJobName;
        Label lblDifficulty;
        Label lblGoal;

        public MissionTitle(string name, int containerWidth)
            : base(name) {
            base.BackColor = Color.Transparent;

            base.Size = new Size(containerWidth - 20, 40);

            lblJobName = new Label("lblJobName");
            lblJobName.Font = FontManager.LoadFont("PMU", 16);
            lblJobName.Location = new Point(15, 0);
            lblJobName.AutoSize = true;
            lblJobName.ForeColor = Color.WhiteSmoke;

            lblDifficulty = new Label("lblDifficulty");
            lblDifficulty.Location = new Point(containerWidth - 40, 5);
            lblDifficulty.Font = FontManager.LoadFont("PMU", 16);
            lblDifficulty.AutoSize = true;
            lblDifficulty.ForeColor = Color.WhiteSmoke;

            lblGoal = new Label("lblGoal");
            lblGoal.Font = FontManager.LoadFont("PMU", 16);
            lblGoal.Location = new Point(lblJobName.X, lblJobName.Y + 16);
            lblGoal.AutoSize = true;
            lblGoal.ForeColor = Color.WhiteSmoke;

            this.AddWidget(lblJobName);
            this.AddWidget(lblDifficulty);
            this.AddWidget(lblGoal);
        }

        public void SetJob(Job job) {
            if (job != null) {
                if (job.Accepted == Enums.JobStatus.Taken) {
                    lblJobName.ForeColor = Color.Yellow;
                    lblGoal.ForeColor = Color.Yellow;
                    lblDifficulty.ForeColor = Color.Yellow;
                } else if (job.Accepted == Enums.JobStatus.Failed) {
                    lblJobName.ForeColor = Color.Red;
                    lblGoal.ForeColor = Color.Red;
                    lblDifficulty.ForeColor = Color.Red;
                } else if (job.Accepted == Enums.JobStatus.Finished) {
                    lblJobName.ForeColor = Color.LightGreen;
                    lblGoal.ForeColor = Color.LightGreen;
                    lblDifficulty.ForeColor = Color.LightGreen;
                } else {
                    lblJobName.ForeColor = Color.WhiteSmoke;
                    lblGoal.ForeColor = Color.WhiteSmoke;
                    lblDifficulty.ForeColor = Color.WhiteSmoke;
                }
                lblJobName.Text = job.Title;
                lblDifficulty.Text = MissionManager.DifficultyToString(job.Difficulty);
                lblDifficulty.Location = new Point(this.Width - lblDifficulty.Width - 40, 0);
                lblGoal.Text = job.GoalName;
            } else {
                lblJobName.ForeColor = Color.Gray;
                lblJobName.Text = "----------";
                lblDifficulty.Text = "";
                lblDifficulty.Location = new Point(this.Width - lblDifficulty.Width - 40, 0);
                lblGoal.Text = "";
            }
        }


    }
}
