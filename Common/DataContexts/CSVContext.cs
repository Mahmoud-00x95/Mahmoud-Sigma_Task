using System.Globalization;
using Core.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Collections.ObjectModel;

namespace Common.DataContexts
{
    public class CSVContext
    {
        protected readonly string filePath = @"..\Common\file.csv";
        protected readonly string fileHeader = "Email,FirstName,LastName,PhoneNumber,TimeInterval,LinkedInProfileUrl,GitHubProfileUrl,TextComment";

        private ObservableCollection<Candidate> _localCandidates = new ObservableCollection<Candidate>();
        private ObservableCollection<Candidate> _candidates { get; set; } = new ObservableCollection<Candidate>();

        public async Task<Candidate?> GetCandidateByEmailAddress(string emailAddress)
        {
            return _candidates.FirstOrDefault(c => c?.Email?.ToLower() == emailAddress?.ToLower());
        }

        public async Task<int> InsertOrUpdateCSVCandidate(Candidate candidate,bool saveOnServer = false)
        {
            
                var _candidate = _candidates.FirstOrDefault(c => c?.Email?.ToLower() == candidate?.Email?.ToLower());
                candidate.IsModified = true;
                if (_candidate != null)
                {
                    _candidates[_candidates.IndexOf(_candidate)] = candidate;
                }
                else
                {
                    _candidates.Add(candidate);
                }

                if (saveOnServer)
                    return await SaveChanges();
                
                return await Task.FromResult(1);
        }

        public async Task ReloadCSVFile()
        {
                _candidates = new ObservableCollection<Candidate>();
                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(fileHeader);
                    }
                    return;
                }
                string[] rows;
                using (var reader = File.OpenText(filePath))
                {
                    var fileText = await reader.ReadToEndAsync();
                    rows = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    if (rows?.Length > 1)
                    {
                        foreach (var row in rows.Skip(1)) // Skip file header
                        {
                            var cols = row.Split(',', '\t');
                            if (cols?.Length > 0)
                            {
                                Candidate candidate = new Candidate();
                                var properties = candidate.GetType().GetProperties();

                                for (int ii = 0; ii < cols.Length - 1; ii++)
                                {
                                    var property = properties[ii];
                                    property.SetValue(candidate, Convert.ChangeType(cols[ii], property.PropertyType), null);
                                }
                                _candidates.Add(candidate);
                            }
                        }
                    }
                }
        }
        private async Task UpdateCSVFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                var rows = _candidates.Select((c) => { c.IsModified = false; return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", c.Email, c.FirstName, c.LastName, c.PhoneNumber, c.TimeInterval, c.LinkedInProfileUrl, c.GitHubProfileUrl, c.TextComment); });
                var data = string.Join("\r\n", rows);
                await writer.WriteAsync(fileHeader + "\r\n" + data);

                // 
                //await Task.Run(async () => {
                //     foreach (var c in _candidates)
                //     {
                //        var row = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", c.Email, c.FirstName, c.LastName, c.PhoneNumber, c.TimeInterval, c.LinkedInProfileUrl, c.GitHubProfileUrl, c.TextComment);
                //        await writer.WriteLineAsync(row);
                //        c.IsModified = false;
                //    }
                // });
            }
        }
        private async Task<int> SaveChanges()
        {
            try
            {
                _localCandidates = _candidates;
                var modifiedLines = _candidates.Where(c => c.IsModified).ToList();

                await ReloadCSVFile();
                modifiedLines.ForEach(async (c) => {
                    await InsertOrUpdateCSVCandidate(c);
                });

                await UpdateCSVFile();

                return await Task.FromResult(1);
            }
            catch (Exception) // in case of any file write exceptions
            {
                _candidates = _localCandidates;
                return await Task.FromResult(0);
            }
        }
    }
}