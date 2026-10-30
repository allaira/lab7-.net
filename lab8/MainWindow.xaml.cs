using lab7;
using System;
using System.Windows;

namespace ConveyorSystem
{
    public partial class MainWindow : Window
    {
        private readonly ConveyorBelt conveyor;
        private readonly Master master;
        private readonly Engineer engineer;
        private readonly Administration admin;

        public MainWindow()
        {
            InitializeComponent();

            conveyor = new ConveyorBelt();
            master = new Master();
            engineer = new Engineer();
            admin = new Administration();

            conveyor.CriticalStop += master.OnCriticalStop;
            conveyor.CriticalStop += engineer.OnCriticalStop;
            conveyor.CriticalStop += admin.OnCriticalStop;

            conveyor.WarningOverload += master.OnWarning;
            conveyor.WarningOverload += engineer.OnWarning;

            conveyor.WeightChecked += OnWeightChecked;

            conveyor.ProcessCompleted += OnProcessCompleted;

            conveyor.MessageGenerated += LogMessage;
            master.MessageGenerated += LogMessage;
            engineer.MessageGenerated += LogMessage;
            admin.MessageGenerated += LogMessage;
        }

        private void BtnCheckClick(object sender, RoutedEventArgs e)
        {
            LstLog.Items.Clear();

            if (double.TryParse(TxtWeight.Text, out double weight))
            {
                try
                {
                    BtnCheck.IsEnabled = false;
                    conveyor.CheckWeight(weight);
                }
                catch (Exception ex)
                {
                    LogMessage($"ПОМИЛКА: {ex.Message}");
                    BtnCheck.IsEnabled = true;
                }
            }
            else
            {
                LogMessage("ПОМИЛКА: Введіть коректне числове значення ваги!");
            }
        }

        private void OnWeightChecked(double weight)
        {
            if (weight > conveyor.MaxAllowedWeight)
                LogMessage($"[Датчик]: УВАГА! Вага {weight} кг перевищує норму (100 кг)!");
            else if (weight > 80)
                LogMessage($"[Датчик]: ПОПЕРЕДЖЕННЯ! Навантаження {weight} кг наближається до критичного!");
            else
                LogMessage($"[Датчик]: Вага {weight} кг. Лінія працює в штатному режимі.");
        }

        private void OnProcessCompleted()
        {
            BtnCheck.IsEnabled = true;
        }

        private void LogMessage(string message)
        {
            LstLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            LstLog.ScrollIntoView(LstLog.Items[LstLog.Items.Count - 1]);
        }
    }
}