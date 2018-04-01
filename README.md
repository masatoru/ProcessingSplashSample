# ProcessingSplashSample
- 下記をテスト
  - https://qiita.com/dhq_boiler/items/930ff2dc960fd950d7a3
- this.Close()だと例外が発生するのでDispatcher.Invokede追加で実行するように修正
- キャンセル処理を追加
- プログレスバーをIsIndeterminateから進捗状況に修正
- ファイル数を回数分処理するように修正