using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomException
{
	public class DuplicateNumberException : Exception
	{
		public DuplicateNumberException(string name)
			: base("Duplicate number is not Accepted! Please input different number!")
		{
		}
	}
}
