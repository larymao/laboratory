# Lary.Laboratory.Twitter
简单封装了Twitter的OAuth验证以及支持视频文件分片上传。

## 支持的功能
- [x] Twitter OAuth  
- [x] Twitter视频分片上传  

## 样例代码
请查看`Lary.Laboratory.Twitter.UnitTests`以获取样例更多样例，此处仅提供部分参考：  
* 视频分片上传
  ```csharp
  /// <summary>
  ///     The test for uploading a media to twitter in chunked upload as an asynchronous operation.
  /// </summary>
  /// <returns>
  ///     The task object representing the asynchronous operation.
  /// </returns>
  [TestMethod]
  public async Task UploadMediaInChunksAsync()
  {
      var _uploader = new MediaUploader(Configs.ConsumerKey, Configs.ConsumerSecret, Configs.AccessToken, Configs.AccessTokenSecret);
      var response = await _uploader.UploadMediaInChunksAsync(Configs.MediaFilepath);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      var jobj = JObject.Parse(response.Data);
      Assert.IsTrue(true);
  
      Console.WriteLine($"{_testContext.TestName} returns: {response.Data}");
  }
  ```