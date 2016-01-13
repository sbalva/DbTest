using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq.Expressions;
using IsoftBaseLib;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DBTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Propertychanged
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields
        private DBConnector connector;
        #endregion

        #region Properties
        private string databaseServer = ".\\MS2005";

        public string DatabaseServer
        {
            get { return databaseServer; }
            set
            {
                databaseServer = value;
                OnPropertyChanged(() => DatabaseServer);
            }
        }

        private string databaseName = "DBTest";

        public string DatabaseName
        {
            get { return databaseName; }
            set
            {
                databaseName = value;
                OnPropertyChanged(() => DatabaseName);
            }
        }

        private string databaseUser = "sa";

        public string DatabaseUser
        {
            get { return databaseUser; }
            set
            {
                databaseUser = value;
                OnPropertyChanged(() => DatabaseUser);
            }
        }

        private string databasePassword = "12345";

        public string DatabasePassword
        {
            get { return databasePassword; }
            set
            {
                databasePassword = value;
                OnPropertyChanged(() => DatabasePassword);
            }
        }

        private int insertCount = 100;

        public int InsertCount
        {
            get { return insertCount; }
            set
            {
                insertCount = value;
                OnPropertyChanged(() => InsertCount);
            }
        }

        private bool isConnected = false;

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                OnPropertyChanged(() => IsConnected);
                OnPropertyChanged(() => ToolsVisible);
            }
        }

        public Visibility ToolsVisible
        {
            get { return (IsConnected) ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool isInserting = false;

        public bool IsInserting
        {
            get { return isInserting; }
            set
            {
                isInserting = value;
                OnPropertyChanged(() => IsInserting);
                OnPropertyChanged(() => InsertingLabelVisible);
                OnPropertyChanged(() => InsertButtonEnabled);
            }
        }

        public Visibility InsertingLabelVisible
        {
            get { return (IsInserting) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool InsertButtonEnabled
        {
            get { return !IsInserting; }
        }

        private Guid updateId;

        public Guid UpdateId
        {
            get { return updateId; }
            set
            {
                updateId = value;
                OnPropertyChanged(() => UpdateId);
            }
        }
        
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }        
        #endregion

        #region Commands
        public ICommand TestDBCommand { get { return new BaseCommand(TestDBMethod); } }
        public ICommand InsertMultipleRecordsCommand { get { return new BaseCommand(InsertMultipleRecordsMethod); } }
        public ICommand UpdateRecordCommand { get { return new BaseCommand(UpdateRecordMethod); } }
        #endregion

        #region Methods
        private void UpdateRecordMethod()
        {
            try
            {
                if (IsConnected && UpdateId != null && UpdateId != Guid.Empty)
                {
                    TestTable table = connector.SelectByKey<TestTable>(UpdateId);
                    if (table != null)
                    {
                        Random random = new Random(DateTime.Now.Second);
                        byte[] array = new byte[5 * 1024 * 1024];
                        random.NextBytes(array);
                        table.TestData = array;
                        int res = connector.Update<TestTable>(table);
                        MessageBox.Show("Total updated - " + res.ToString());
                    }
                    else
                        MessageBox.Show("Запись не найдена", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void InsertMultipleRecordsMethod()
        {
            if (IsConnected && InsertCount > 0)
            {
                IsInserting = true;
                new Task(new Action(() =>
                {
                    Random random = new Random(DateTime.Now.Second);
                    int res = 0;
                    for (int i = 0; i < InsertCount; i++)
                    {
                        try
                        {
                            byte[] array = new byte[1024 * 1024];
                            random.NextBytes(array);
                            TestTable table = new TestTable(array);
                            res = res + connector.Insert<TestTable>(table);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine("[DBTest] Insert Exception "+ex.ToString());
                        }
                    }

                    App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            IsInserting = false;
                            MessageBox.Show("Total inserted - "+ res.ToString());
                        }), null);
                })).Start();
            }
        }

        private void TestDBMethod()
        {
            try
            {
                connector = new DBConnector(DatabaseServer, DatabaseName, DatabaseUser, DatabasePassword);
                if (connector.TestConnection())
                {
                    IsConnected = true;
                }
                else
                    MessageBox.Show("Не удалось установить подключение к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка подключения к базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region XAML Events
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = (e.Key == Key.Space);
        }
        #endregion
    }
}
