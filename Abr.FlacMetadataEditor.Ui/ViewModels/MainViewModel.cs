using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Abr.FlacMetadataEditor.Logic;
using Abr.FlacMetadataEditor.Ui.Helpers;

namespace Abr.FlacMetadataEditor.Ui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events
        
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
        
        #region Fields
        
        private List<FileInfo?>? _files;
        private FileInfo? _selectedFile;
        private string? _author;
        private string? _title;
        private RelayCommand? _loadDirectoryCommand;
        private RelayCommand? _saveFileCommand;

        #endregion
        
        #region Properties
        
        /// <summary>
        /// List of Files contained in selected directory
        /// </summary>
        public List<FileInfo?>? Files
        {
            get => _files;
            set
            {
                _files = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current selected file from <see cref="Files"/>
        /// </summary>
        public FileInfo? SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                OnPropertyChanged();
                
                OnSelectedFileChanged();
            }
        }
        
        /// <summary>
        /// Author of the <see cref="SelectedFile"/>
        /// </summary>
        public string? Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Title of the <see cref="SelectedFile"/>
        /// </summary>
        public string? TitleMus
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Shows dialog for selecting directory
        /// </summary>
        public RelayCommand LoadDirectoryCommand
        {
            get => _loadDirectoryCommand ??= new RelayCommand( _ => LoadDirectory());
        }
        
        /// <summary>
        /// Save changes for file
        /// </summary>
        public RelayCommand SaveFileCommand
        {
            get => _saveFileCommand ??= new RelayCommand(_ => SaveFile());
        }
        
        #endregion
        
        #region EventHandler
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void OnSelectedFileChanged()
        {
            if (SelectedFile == null) return;

            Author = AudioFileInterface.GetArtistName(SelectedFile.FullName);
            TitleMus = AudioFileInterface.GetTitle(SelectedFile.FullName);
        }
        
        #endregion
        
        #region Methods
        
        private void LoadDirectory()
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;
            
            var selectedFolder = dialog.SelectedPath;

            if (string.IsNullOrWhiteSpace(selectedFolder))
                return;

            var folderInfo = new DirectoryInfo(selectedFolder);

            if (!folderInfo.Exists)
                return;

            Files = folderInfo.GetFiles().ToList()!;
        }
        
        private void SaveFile()
        {
            try
            {
                AudioFileInterface.ChangeInformation(SelectedFile.FullName, Author, TitleMus);

                var destFileName = $"{Author} - {TitleMus}{SelectedFile.Extension}";
                var sourceFilePath = SelectedFile.Directory.FullName;
                var destFile = Path.Combine(sourceFilePath, destFileName);
            
                SelectedFile.MoveTo(destFile);
            }
            finally
            {
                var nextFileIndex = Files.IndexOf(SelectedFile)+1;
                var nextFile = Files[nextFileIndex];
                SelectedFile = nextFile;
            }            
        }
        
        #endregion
    }
}