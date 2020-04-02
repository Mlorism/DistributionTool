using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.ViewModels
{
	abstract class BaseViewModel : INotifyPropertyChanged, ITab
	{
		public string TabName { get; set; }

		#region INotifyPropertyChanged
		/// <summary>
		/// INotifyPropertyChanged implementation
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChange (string propertyName)
		{
			try
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}

			catch (Exception)
			{
				throw new Exception("Invalid property name: " + propertyName);
			}
		} //OnPropertyChange()
		#endregion
	}
}
