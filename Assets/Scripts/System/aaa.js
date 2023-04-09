// 操作方法：
// 十字キーで移動できます。
// 移動できない場合は、キャンバスを1回クリックしてください。

// Press arrow keys to move.
// If nothing happens, click the canvas once.

class Area {
  /**
   * @param {Area} p 親エリア parent
   * @param {Array.<Area>} c 子エリア children (0 or 2 areas)
   * @param {number} x エリアの左上のX座標 x position in tiles. anchor is top-left.
   * @param {number} y エリアの左上のY座標 y position
   * @param {number} w エリアの幅 width
   * @param {number} h エリアの高さ height
   */
  constructor(p, c, x, y, w, h) {
    // ※
    // ↓と同じ意味です。 Same as below.
    // this.p = p;
    // this.c = c;
    // ～略～
    // this.h = h;
    Object.assign(this, { p, c, x, y, w, h });
  }

  /**
   * デバッグツールでいちいち c[0] と描くのが面倒なので別名をつける
   * Just a shorthand for DevTools Console.
   */
  get a() {
    return this.c[0];
  }

  /**
   * デバッグツールでいちいち c[1] と描くのが面倒なので別名をつける
   * Just a shorthand for DevTools Console.
   */
  get b() {
    return this.c[1];
  }

  /**
   * 5x5より小さいエリアができるのを防ぐ
   * Avoid to create areas smaller than 5x5.
   */
  get minAreaWidth() {
    return 5;
  }

  /**
   * @param {number} d 分割するX座標 Split position at tiles.
   * @returns {boolean} 分割に成功したらtrue, 失敗したらfalse. true=succeed, false=failed.
   */
  splitAtX(d) {
    let t = this,
      m = t.minAreaWidth;
    if (d < m || t.w - d < m) return false;
    t.c = [new Area(t, [], t.x, t.y, d, t.h), new Area(t, [], t.x + d, t.y, t.w - d, t.h)];
    return true;
  }

  /**
   * @param {number} d 分割するY座標 Split position at tiles.
   * @returns {boolean} 分割に成功したらtrue, 失敗したらfalse. true=succeed, false=failed.
   */
  splitAtY(d) {
    let t = this,
      m = t.minAreaWidth;
    if (d < m || t.h - d < m) return false;
    t.c = [new Area(t, [], t.x, t.y, t.w, d), new Area(t, [], t.x, t.y + d, t.w, t.h - d)];
    return true;
  }

  /**
   * このエリアを、ランダムに再帰的に分割する。
   * split this area randomly and recursive.
   */
  splitRandomly() {
    let t = this;
    let m = this.minAreaWidth;
    let d1 = floor(random(m, t.w - m));
    let d2 = floor(random(m, t.h - m));
    let ok = random() > 0.5 ? t.splitAtX(d1) : t.splitAtY(d2);
    if (ok) for (let a of t.c) a.splitRandomly();
  }

  // ※
  // ユーザー定義の反復可能オブジェクト(User-defined iterables)
  // https://developer.mozilla.org/ja/docs/Web/JavaScript/Guide/Iterators_and_Generators#User-defined_iterables

  /**
   * User-defined iterables.
   * 例えば、デバッグツールで
   * for(let a of r) console.log(a)
   * とすると、存在する全てのエリアをConsoleに出力できる。
   */
  *[Symbol.iterator]() {
    let t = this,
      s = [],
      m = [];
    s.push(t);
    m.push(t);
    while (s.length !== 0) {
      let a = s.pop();
      yield a; // DFS（深さ優先探索）で全てのエリアがyieldされる
      for (let h of a.c) {
        if (!m.includes(h)) {
          m.push(h);
          s.push(h);
        }
      }
    }
  }

  /**
   * @returns {number} 一番上の親を0, その子を1, さらにその子を2... とした、「エリアの深さ」
   */
  get depth() {
    let d = 0,
      i = this;
    while (i.p !== null) {
      d++;
      i = i.p;
    }
    return d;
  }

  /**
   * @returns {number} 親から見て、何番目の子であるかを返す。通常0または1だが、親がいない場合-1を返す。
   */
  get nthChildren() {
    let t = this;
    return t.p === null ? -1 : t.p.c.indexOf(t);
  }

  /**
   * @returns {boolean} 自分および自分の先祖をたどっていき、vがあればtrue、なければfalse
   * @param {Area} v
   */
  hasAncestor(v) {
    let t = this;
    if (t === v) {
      return true;
    } else if (t.p === null) {
      return false;
    } else if (t.p === v) {
      return true;
    } else {
      return t.p.hasAncestor(v);
    }
  }

  /**
   * @returns {string} 自分がツリーの根本なら'R', その子なら'RA'か'RB', 'RA'の子なら'RAA'か'RAB'... を返す
   * returns 'R' if this is the root of a tree,
   * 'RA' or 'RB' if this is one of children of the root,
   * 'RAA' or 'RAB' if this is one of children of 'RA', ... (recursive)
   * @param {string} s （再帰中だけ使う） / only for recursion purpose
   */
  asLabel(s = "") {
    // ※
    // 引数にイコールをつけると、デフォルト引数(default parameters)となります
    // https://developer.mozilla.org/ja/docs/Web/JavaScript/Guide/Functions

    let t = this;
    // ※ 式 'AB'[i] は、iが0なら'A', iが1なら'B'を返す
    return t.p === null ? "R" + s : t.p.asLabel("AB"[t.nthChildren] + s);
  }

  /**
   * @param {Camera} camera normally game.camera
   * @param {number} m 描画する最大の深さ max depth
   * @param {number} d 現在の深さ（再帰中だけ使う） only for recursion purpose
   */
  draw(camera, m = 9, d = 0) {
    push();
    let t = this;
    let c = camera;
    let w = c.tileWidth;
    let cx = w * c.x,
      cy = w * c.y;
    //----- 矩形を描画 / Draw this area
    noFill();
    stroke("black");
    strokeWeight(4);
    rect(w * t.x - cx, w * t.y - cy, w * t.w, w * t.h);

    //----- RA,RBAなどのラベルを描画 / Draw labels like RA,RBA.
    textAlign(LEFT, TOP);
    fill("white");
    textSize(30);
    textStyle(BOLD);
    // まだ子がいるのに描画限界のため描画できない時は、'...'を末尾につける
    // add '...' if this area have chilren yet cannot draw further.
    let continues = d === m && t.c.length !== 0 ? "..." : "";
    // Stringクラスのreplace関数を使い、4文字ごとに改行する
    // Insert a linebreak every 4 characters.
    let label = t.asLabel().replace(/(.{4})/g, "$1\n") + continues;
    text(label, w * t.x + 6 - cx, w * t.y + 6 - cy);
    pop();
    // 子を再帰的に描画する
    // Draw children recursive
    if (d < m) for (let a of t.c) a.draw(camera, m, d + 1);
  }
}

/**
 * 部屋をあらわすクラス
 */
class Room {
  /**
   * @param {number} x 部屋の左上端のx座標 x position in tiles. (anchor is top-left)
   * @param {number} y 部屋の左上端のy座標 y position
   * @param {number} w 部屋の幅 width
   * @param {number} h 部屋の高さ height
   * @param {Area} a どのエリア内の部屋であるか the area contains me
   */
  constructor(x, y, w, h, a) {
    Object.assign(this, { x, y, w, h, a });
  }
  /**
   * 部屋の範囲に矩形を描く。デバッグ用. only for debug purpose
   * @param {Camera} camera
   */
  draw(camera) {
    let t = this;
    let [cx, cy] = camera.screenPos;
    let w = camera.tileWidth;
    push();
    strokeWeight(6);
    noFill();
    stroke("black");
    rect(w * t.x - cx, w * t.y - cy, w * t.w, w * t.h);
    pop();
  }
}

/**
 * ダンジョンの1階層をあらわすクラス
 */
class Level {
  constructor() {
    /** タイルマップ。0は床、1は壁をあらわす. tilemap. 0=ground, 1=wall */
    this.tiles = [
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
      1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
    ];
    /** タイルマップの横幅. tilemap width */
    this.lenX = 20;
    /** タイルマップの縦幅. tilemap height */
    this.lenY = 20;
  }
  /**
   * @param {number} x
   * @param {number} y
   */
  xyToIndex(x, y) {
    if (x < 0 || x >= this.lenX || y < 0 || y >= this.lenY) return -1;
    return y * this.lenX + x;
  }
  /**
   * @returns {number} 座標(x,y)のタイルの番号を返す。(x,y)が範囲外なら1を返す。
   * @param {number} x
   * @param {number} y
   */
  tileAt(x, y) {
    let i = this.xyToIndex(x, y);
    return i === -1 ? 1 : this.tiles[i];
  }
  /**
   * @param {number} x
   * @param {number} y
   * @param {number} n
   * @returns {boolean} 0=succeed, 1=failed
   */
  putTile(x, y, n) {
    let i = this.xyToIndex(x, y);
    if (i === -1) return false;
    this.tiles[i] = n;
    return true;
  }
}

/**
 * プレイヤーや敵キャラなどをあらわすクラス
 */
class Actor {
  constructor(x, y, image) {
    this.x = x;
    this.y = y;
    this.image = image;
  }
}

/**
 * カメラのクラス
 */
class Camera {
  /**
   * @param {number} x カメラのX座標。単位はタイル pos x in tiles.
   * @param {number} y カメラのY座標。単位はタイル pos y.
   * @param {number} tileWidth 1タイルの辺のピクセル数
   */
  constructor(x, y) {
    this.x = x;
    this.y = y;
    this.tileWidth = 20;
  }
  /**
   * スクリーン座標系における、カメラの位置を返す
   */
  get screenPos() {
    return [this.tileWidth * this.x, this.tileWidth * this.y];
  }
}

/**
 * 移動コマンド
 */
class ComMove {
  /**
   * @param {Actor} actor 移動させたいアクター
   * @param {number} dx 何マス移動するか delta x in tiles
   * @param {number} dy 何マス移動するか delta y
   */
  constructor(actor, dx, dy) {
    let t = this;
    t.actor = actor;
    t.dx = dx;
    t.dy = dy;
    t.beginX = -1;
    t.beginY = -1;
    t.endX = -1;
    t.endY = -1;
    /** 実行したフレーム数 frames executed */
    t.f = 0;
  }
  /**
   * コマンドを1フレーム実行する
   */
  exec() {
    let t = this;
    if (t.done) return t.done; //終了しているコマンドは実行しない
    t.f++;
    if (t.f === 1) {
      // 開始地点と終了地点の座標を計算
      t.beginX = t.actor.x;
      t.beginY = t.actor.y;
      t.endX = t.actor.x + t.dx;
      t.endY = t.actor.y + t.dy;
    }
    // ↑で計算した座標の間を移動する linear interpolation
    t.actor.x = t.beginX + (t.f * t.dx) / 15;
    t.actor.y = t.beginY + (t.f * t.dy) / 15;

    return t.done;
  }
  /**
   * @returns {boolean} コマンドが終了していればtrue, 実行中ならfalse
   */
  get done() {
    return this.f >= 15;
  }
}

/**
 * ゲームの状態や、ロジックをまとめたクラス
 */
class Game {
  constructor() {
    this.level = new Level();
    this.area = new Area(null, [], 0, 0, this.level.lenX, this.level.lenY);
    this.player = null;
    this.actors = [];
    this.camera = new Camera(0, 0);
    this.commands = [];

    this.areaShown = true;
  }
  /**
   * @param {Area} area
   */
  static makeRooms(area) {
    // 矩形の葉ノード（下端の矩形）を抽出
    // leaf nodes must have a room, so we create rooms into it
    let leafs = Array.from(area).filter((a) => a.c.length === 0);
    let rooms = leafs.map((L) => {
      let w = floor(random(2, L.w - 2));
      let h = floor(random(2, L.h - 2));
      let x = L.x + floor(random(2, L.w - w));
      let y = L.y + floor(random(2, L.h - h));
      return new Room(x, y, w, h, L);
    });
    return rooms;
  }
  /** デバッグツール用. Shorthand for DevTool Console */
  makeRooms() {
    this.rooms = Game.makeRooms(this.area);
    return this.rooms;
  }
  /**
   * @param {Array.<Room>} rooms
   * @param {Level} level
   */
  static applyRoomsToLevel(rooms, level) {
    for (let r of rooms) {
      for (let y = r.y; y < r.y + r.h; y++) {
        for (let x = r.x; x < r.x + r.w; x++) {
          level.putTile(x, y, 0);
        }
      }
    }
    return level;
  }
  /** デバッグツール用. Shorthand for DevTool Console */
  applyRoomsToLevel() {
    Game.applyRoomsToLevel(this.rooms, this.level);
    return this.level;
  }
  /**
   * areaとroomsをもとに、levelへ通路をひく
   * Create passages from area and rooms, and modify tilemap of a level with passages
   * @param {Area} area
   * @param {Array.<Room>} rooms
   * @param {Level} level
   */
  static applyPassagesToLevel(area, rooms, level) {
    for (let a of area) {
      if (a.c.length === 0) continue;
      let c0 = a.c[0];
      let c1 = a.c[1];
      let rooms0 = rooms.filter((r) => r.a.hasAncestor(c0));
      let rooms1 = rooms.filter((r) => r.a.hasAncestor(c1));
      if (c0.y === c1.y) {
        //----- エリアが横に並んでいる場合 if areas are horizontal..
        // 境界線のX座標 border x pos
        let d = c0.x + c0.w;
        // 部屋r0,r1のどちらが境界線に近いか調べる関数 distance function
        let f0 = (r0, r1) => abs(r0.x + r0.w - d) - abs(r1.x + r1.w - d);
        let f1 = (r0, r1) => abs(r0.x - d) - abs(r1.x - d);
        // それぞれのエリアの、境界線に一番一番近い部屋
        // n0,n1 = nearest rooms in 2 areas
        let n0 = rooms0.sort(f0)[0];
        let n1 = rooms1.sort(f1)[0];
        // 通路をひく
        let o0 = floor(random(n0.y, n0.y + n0.h)); //offset
        let o1 = floor(random(n1.y, n1.y + n1.h));
        for (let x = n0.x + n0.w; x < d; x++) level.putTile(x, o0);
        for (let x = n1.x; x > d; x--) level.putTile(x, o1);
        for (let y = min(o0, o1); y <= max(o0, o1); y++) level.putTile(d, y);
      } else {
        //----- エリアが縦に並んでいる場合 if areas are vertical..
        let d = c0.y + c0.h;
        let f0 = (r0, r1) => abs(r0.y + r0.h - d) - abs(r1.y + r1.h - d);
        let f1 = (r0, r1) => abs(r0.y - d) - abs(r1.y - d);
        let n0 = rooms0.sort(f0)[0];
        let n1 = rooms1.sort(f1)[0];
        let o0 = floor(random(n0.x, n0.x + n0.w));
        let o1 = floor(random(n1.x, n1.x + n1.w));
        for (let y = n0.y + n0.h; y < d; y++) level.putTile(o0, y);
        for (let y = n1.y; y > d; y--) level.putTile(o1, y);
        for (let x = min(o0, o1); x <= max(o0, o1); x++) level.putTile(x, d);
      }
    }
  }
  applyPassagesToLevel(area, rooms, level) {
    Game.applyPassagesToLevel(this.area, this.rooms, this.level);
  }
}
let game;

/**
 * p5.js の準備ができた直後に計算される関数
 * called by p5.js once
 */
function setup() {
  // ゲームの状態を初期化
  game = new Game();

  // プレイヤーを作る
  let player = new Actor(2, 2, "🐤");
  game.player = player;

  // 敵を作る
  let enemy = new Actor(2, 1, "🦗");

  // 初期配置のアクター
  game.actors = [player, enemy];

  // キャンバスを作る
  createCanvas(480, 480);

  //----- ボタンを配置
  let makeButton = (onPressed, label) => {
    let button = createButton("");
    button.style("font-size", "2em");
    button.style("margin-top", "0.2em");
    button.style("display", "block");
    button.html(label);
    button.mousePressed((_) => onPressed(button));
    return button;
  };
  let b = makeButton((button) => {
    game.level = new Level();
    game.area.splitRandomly();
    game.makeRooms();
    game.applyRoomsToLevel();
    game.applyPassagesToLevel();
  }, "ダンジョン生成 Make dungeon");
  let b2 = makeButton((button) => {
    let t = game.camera.tileWidth;
    game.camera.tileWidth = t === 20 ? 60 : 20;
  }, "カメラ変更 Switch camera");
  let b3 = makeButton((button) => {
    game.areaShown = !game.areaShown;
  }, "矩形表示 Show BSP areas");
}

/**
 * p5.js によって毎フレーム計算される関数
 * called by p5.js every frame
 */
function draw() {
  // 1マスの大きさ
  let w = game.camera.tileWidth;

  // プレイヤーの入力を受け付ける
  // player input
  if (keyIsPressed && game.commands.length === 0) {
    let dxy = { 37: [-1, 0], 38: [0, -1], 39: [1, 0], 40: [0, 1] }[keyCode];
    if (dxy !== undefined) {
      game.commands.push(new ComMove(game.player, dxy[0], dxy[1]));

      // 仮に、敵を移動させてみる
      // try synced move. will be deleted soon
      game.commands.push(new ComMove(game.actors[1], 0, 1));
    }
  }

  // コマンドをすべて1フレーム分実行する
  for (let c of game.commands) {
    c.exec();
  }
  // 実行し終わったコマンドを消す
  game.commands = game.commands.filter((c) => !c.done);

  // カメラを、プレイヤーが画面の中央へ来るよう調整
  let p = game.player;
  let c = game.camera;
  c.x = p.x - 7 / 2;
  c.y = p.y - 7 / 2;
  let cx = w * c.x;
  let cy = w * c.y;

  // キャンバスを背景色で塗りつぶす
  background("Bisque");
  // レベル（ダンジョンの1階層）を描画
  textAlign(LEFT, TOP);
  textSize((w * 7) / 8);
  for (let y = 0; y < game.level.lenY; y++) {
    for (let x = 0; x < game.level.lenX; x++) {
      let t = game.level.tileAt(x, y);
      if (t === 1) {
        text("🌳", w * x - cx, w * y - cy);
      }
    }
  }

  if (game.areaShown) game.area.draw(game.camera);

  // アクター（主人公や敵キャラなど）を描画
  for (let a of game.actors) {
    text(a.image, w * a.x - cx, w * a.y - cy);
  }
}
