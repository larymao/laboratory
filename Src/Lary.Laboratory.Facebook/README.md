# Lary.Laboratory.Facebook
一个`.NET Core`版本的简单的Facebook SDK，提供**部分**Facebook功能（主要是小醉魔同学在日常用到的部分，至于其他部分……好吧，他不怎么勤快你懂的）  

## 支持的功能
[x] 简单的帖子发布（立即发布、存为草稿、定时发布）、点赞、获取评论和点赞情况  
[x] 支持图片/视频上传（视频支持分片上传）  
[x] 支持广告帖的发布（带上Headline、Description、Call2Action，很愉快……）  

## 样例代码
请查看`Lary.Laboratory.Facebook.UnitTests`以获取样例更多样例，此处仅提供部分参考：  
* 帖子发布、点赞、获取评论和点赞情况
  ```csharp
  /// <summary>
  ///     Test for publishing a text only post to facebook, liking it and getting its comments, reaction 
  ///     as an asynchronous operation.
  /// </summary>
  /// <returns>
  ///     The task object representing the asynchronous operation.
  /// </returns>
  [TestMethod]
  public async Task TextPostMultiTestsAsync()
  {
      // Publishes post.
      var post = new Post
      {
          Message = Guid.NewGuid().ToString("N")
      };
  
      var response = await post.PublishAsync(TestsBase.PageId, TestsBase.PageAccessToken);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      var jobj = JObject.Parse(response.Data);
      Assert.IsTrue(jobj["id"] != null && !String.IsNullOrEmpty(jobj["id"].ToString()));
  
      post.Id = jobj["id"].ToString();
      Console.WriteLine($"PostID: {post.Id}.");
  
      // Likes post.
      response = await post.LikeAsync(TestsBase.PageAccessToken);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      jobj = JObject.Parse(response.Data);
      Assert.IsTrue(jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true);
  
      // Gets Comments.
      response = await post.GetCommentsAsync(TestsBase.PageAccessToken);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      jobj = JObject.Parse(response.Data);
      Assert.IsTrue(jobj["data"] != null);
  
      // Gets reactions.
      response = await post.GetReactionsAsync(TestsBase.PageAccessToken);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      jobj = JObject.Parse(response.Data);
      Assert.IsTrue(jobj["data"] != null && jobj["data"].Count() > 0);
  }
  ```

* 视频上传
  ```csharp
  /// <summary>
  ///     The test for uploading a video to facebook by resumable upload as an asynchronous operation.
  /// </summary>
  /// <returns>
  ///     The task object representing the asynchronous operation.
  /// </returns>
  [TestMethod]
  public async Task UploadInChunksAsync()
  {
      var response = await VideoUploader.UploadInChunksAsync(TestsBase.TestVideoPath, TestsBase.PageId, TestsBase.PageAccessToken, _request);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      var jobj = JObject.Parse(response.Data);
      Assert.IsTrue((jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true)
          && (jobj["video_id"] != null && !String.IsNullOrEmpty(jobj["video_id"].ToString())));
  }
  ```

* 发布视频广告帖并自定义视频封面图
  ```csharp
  /// <summary>
  ///     Tests the publish of facebook video ad with custom video cover as an asynchronous operation.
  /// </summary>
  /// <returns>
  ///     The task object representing the asynchronous operation.
  /// </returns>
  [TestMethod]
  public async Task PublishVieoAdWithCustomCoverAsync()
  {
      var request = new AdCreativeCreatingRequest
      {
          ObjectStorySpec = new ObjectStorySpec
          {
              PageId = TestsBase.PageId,
              VideoData = new VideoData
              {
                  Call2Action = new Call2Action
                  {
                      Type = Call2ActionType.LEARN_MORE,
                      Value = new Call2ActionValue
                      {
                          Link = TestsBase.Link
                      }
                  },
                  Headline = "Headline of Video Ad With Custom Cover",
                  Message = Guid.NewGuid().ToString("N")
              }
          }
      };
  
      var response = await AdCreativePublisher.PublishVideoAdAsync(TestsBase.TestVideoPath, TestsBase.TestPicturePath, request, TestsBase.AdAccountId, TestsBase.AccessToken, TestsBase.PageAccessToken, true);
      Assert.IsTrue(response.Code == ResponseCode.SUCCESS, response.ReasonPhrase ?? String.Empty);
  
      var jobj = JObject.Parse(response.Data);
      Assert.IsTrue(jobj["success"] != null && Boolean.Parse(jobj["success"].ToString()) == true);
  }
  ```