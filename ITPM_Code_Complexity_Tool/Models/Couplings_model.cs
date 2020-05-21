using System;
using System.Collections.Generic;
using System.Text;

namespace ITPM_Code_Complexity_Tool.Models
{
	public class Couplings_model
	{
		int lineNumber, regular_global, recursive_global, recursive, regular_regular, regular_recursive, recursive_regular, recursive_recursive;
		String line;
		public int getLineNumber()
		{
			return lineNumber;
		}
		public void setLineNumber(int lineNumber)
		{
			this.lineNumber = lineNumber;
		}
		public int getRegular_global()
		{
			return regular_global;
		}
		public void setRegular_global(int regular_global)
		{
			this.regular_global = regular_global;
		}
		public int getRecursive_global()
		{
			return recursive_global;
		}
		public void setRecursive_global(int recursive_global)
		{
			this.recursive_global = recursive_global;
		}
		public int getRecursive()
		{
			return recursive;
		}
		public void setRecursive(int recursive)
		{
			this.recursive = recursive;
		}
		public int getRegular_regular()
		{
			return regular_regular;
		}
		public void setRegular_regular(int regular_regular)
		{
			this.regular_regular = regular_regular;
		}
		public int getRegular_recursive()
		{
			return regular_recursive;
		}
		public void setRegular_recursive(int regular_recursive)
		{
			this.regular_recursive = regular_recursive;
		}
		public int getRecursive_regular()
		{
			return recursive_regular;
		}
		public void setRecursive_regular(int recursive_regular)
		{
			this.recursive_regular = recursive_regular;
		}
		public int getRecursive_recursive()
		{
			return recursive_recursive;
		}
		public void setRecursive_recursive(int recursive_recursive)
		{
			this.recursive_recursive = recursive_recursive;
		}
		public String getLine()
		{
			return line;
		}
		public void setLine(String line)
		{
			this.line = line;
		}

	}
}
