using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
	public interface ICacheService
	{
		/// <summary>
		/// Get Data using key
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		T GetData<T>(string key);

		/// <summary>
		/// Set Data with Value and Expiration Time of Key
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		bool SetData<T>(string key, T value);

		/// <summary>
		/// Remove Data
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		object RemoveData(string key);
	}
}
