﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DistributionTool.Windows
{
	/// <summary>
	/// Interaction logic for ConfirmWindow.xaml
	/// </summary>
	public partial class ConfirmWindow
	{
		#region Properties
		private bool answer;		

		/// <summary>
		/// User answer yes = true, no = false
		/// </summary>
		#endregion

		public ConfirmWindow(string title, string question)
		{
			InitializeComponent();
			this.QuestionText.Text = question;
			this.Title = title;
		}

		#region Methods
		
		public bool AskQuestion()
		{			
			this.ShowDialog();
			this.Focus();
			
			return answer;
		}

		private void Yes_button_Click(object sender, RoutedEventArgs e)
		{
			answer = true;
			this.Close();
		} // Yes_button_Click() sends true to Answer property

		private void No_button_Click(object sender, RoutedEventArgs e)
		{
			answer = false;
			this.Close();
		} // No_button_Click() sends false to Answer property

		#endregion

	}
}
