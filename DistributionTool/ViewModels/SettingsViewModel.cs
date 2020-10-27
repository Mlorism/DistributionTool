using DistributionTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Windows;
using System.ComponentModel;

namespace DistributionTool.ViewModels
{
	class SettingsViewModel : BaseViewModel, ITab
	{
		#region Properties
		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

		private static bool amberTheme { get; set; }
		public static bool AmberTheme 
		{
			get { return amberTheme; }
			set
			{
				amberTheme = value;				
				if(value == true)
				{
					ChangeColour("Amber");					
				}
				
			} 
		}

		private static bool limeTheme { get; set; }
		public static bool LimeTheme
		{
			get { return limeTheme; }
			set
			{
				limeTheme = value;				
				if (value == true)
				{
					ChangeColour("Lime");					
				}
				
			}
		}

		private static bool steelTheme { get; set; }
		public static bool SteelTheme
		{
			get { return steelTheme; }
			set
			{
				steelTheme = value;				
				if (value == true)
				{
					ChangeColour("Steel");					
				}
				
			}
		}

		private static bool tealTheme { get; set; } = true;
		public static bool TealTheme
		{
			get { return tealTheme; }
			set
			{
				tealTheme = value;				
				if (value == true) 
				{ 
				ChangeColour("Teal");				
				} 
			}
		} 
		#endregion


		#region Constructor
		public SettingsViewModel()
		{
			TabName = "Settings";
		}
		#endregion

		#region Methods		
		public static void ChangeColour(string color)
		{
			ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(color), ThemeManager.GetAppTheme("BaseLight"));			
		}

		#endregion



	}
}
