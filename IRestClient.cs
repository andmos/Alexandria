using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


	public interface IRestClient<T> where T : class
	{
		Task<T> Get(string id);
		Task<IList<T>> GetAll();
	}


