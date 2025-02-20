﻿
namespace YooAsset
{
	internal class DatabaseRawFileProvider : ProviderBase
	{
		public override float Progress
		{
			get
			{
				if (IsDone)
					return 1f;
				else
					return 0;
			}
		}

		public DatabaseRawFileProvider(AssetSystemImpl impl, string providerGUID, AssetInfo assetInfo) : base(impl, providerGUID, assetInfo)
		{
		}
		public override void Update()
		{
#if UNITY_EDITOR
			if (IsDone)
				return;

			if (Status == EStatus.None)
			{
				// 检测资源文件是否存在
				string guid = UnityEditor.AssetDatabase.AssetPathToGUID(MainAssetInfo.AssetPath);
				if (string.IsNullOrEmpty(guid))
				{
					Status = EStatus.Fail;
					LastError = $"Not found asset : {MainAssetInfo.AssetPath}";
					YooLogger.Error(LastError);
					InvokeCompletion();
					return;
				}

				Status = EStatus.Checking;

				// 注意：模拟异步加载效果提前返回
				if (IsWaitForAsyncComplete == false)
					return;
			}

			if(Status == EStatus.Checking)
			{
				RawFilePath = MainAssetInfo.AssetPath;
				Status = EStatus.Success;
				InvokeCompletion();
			}
#endif
		}
	}
}