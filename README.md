# ProcessingSplashSample
- 下記をベースに作らせていただきました
  - https://qiita.com/dhq_boiler/items/930ff2dc960fd950d7a3
- this.Close()だと例外が発生するのでDispatcher.Invokeで追加で実行するように修正
- キャンセル処理を追加
- プログレスバーをIsIndeterminateから進捗状況に修正
- （仮の引数としての）ファイル数分を処理するように修正

![](./asset/image1.png)

