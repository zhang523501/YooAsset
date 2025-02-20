﻿using System.IO;
using System.Text;

namespace YooAsset
{
	public class RawFileOperationHandle : OperationHandleBase
	{
		private System.Action<RawFileOperationHandle> _callback;

		internal RawFileOperationHandle(ProviderBase provider) : base(provider)
		{
		}
		internal override void InvokeCallback()
		{
			_callback?.Invoke(this);
		}

		/// <summary>
		/// 完成委托
		/// </summary>
		public event System.Action<RawFileOperationHandle> Completed
		{
			add
			{
				if (IsValid == false)
					throw new System.Exception($"{nameof(RawFileOperationHandle)} is invalid");
				if (Provider.IsDone)
					value.Invoke(this);
				else
					_callback += value;
			}
			remove
			{
				if (IsValid == false)
					throw new System.Exception($"{nameof(RawFileOperationHandle)} is invalid");
				_callback -= value;
			}
		}

		/// <summary>
		/// 等待异步执行完毕
		/// </summary>
		public void WaitForAsyncComplete()
		{
			if (IsValid == false)
				return;
			Provider.WaitForAsyncComplete();
		}

		/// <summary>
		/// 释放资源句柄
		/// </summary>
		public void Release()
		{
			this.ReleaseInternal();
		}
		

		/// <summary>
		/// 获取原生文件的二进制数据
		/// </summary>
		public byte[] GetRawFileData()
		{
			if (IsValid == false)
				return null;
			string filePath = Provider.RawFilePath;
			if (File.Exists(filePath) == false)
				return null;
			return File.ReadAllBytes(filePath);
		}

		/// <summary>
		/// 获取原生文件的文本数据
		/// </summary>
		public string GetRawFileText()
		{
			if (IsValid == false)
				return null;
			string filePath = Provider.RawFilePath;
			if (File.Exists(filePath) == false)
				return null;
			return File.ReadAllText(filePath, Encoding.UTF8);
		}

		/// <summary>
		/// 获取原生文件的路径
		/// </summary>
		public string GetRawFilePath()
		{
			if (IsValid == false)
				return string.Empty;
			return Provider.RawFilePath;
		}
	}
}