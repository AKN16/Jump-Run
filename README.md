# 🎮 Jump-Run

**[English](#english) | [Tiếng Việt](#tiếng-việt)**

---

## English

A 2D pixel-art platformer built with **Unity**. Guide the player through obstacles (traps, a river, patrolling enemies), collect coins and a key to reach victory.

**🔗 Play online:** [https://akn16.github.io/Jump-Run/](https://akn16.github.io/Jump-Run/)

### 📸 Screenshots

*(Add gameplay screenshots here)*

### 🕹️ Gameplay

- **Move & jump** across platforms, including a moving platform
- **Collect coins** scattered across the map
- **Find and grab the key** to complete the objective
- **Avoid traps** — touching one ends the game
- **Falling into the river** — instant Game Over
- **Patrolling enemies** move along a fixed path; when the player gets close they **chase**, then return to their original spot if they chase too far
- **Camera follows the player**, confined to the map bounds via Cinemachine Confiner 2D — never shows outside the level

### 🛠️ Tech stack

| Component | Technology |
|---|---|
| Engine | Unity 6 |
| Language | C# |
| Camera | Cinemachine 3.x |
| Level design | Tilemap (Grid, Ground/ForeGround/BackGround, River) |
| Physics | Rigidbody2D, Tilemap Collider 2D, Composite Collider 2D |
| Build target | WebGL (hosted via GitHub Pages) |

### 📁 Project structure

```
Assets/          → Scripts, Scenes, Prefabs, Animations...
ProjectSettings/ → Unity project configuration (tags, layers, physics...)
Packages/        → Unity packages used
TaiNguyen/        → Source assets: sprites, audio, fonts (with LICENSE & CREDITS)
docs/            → WebGL build output, served via GitHub Pages
```

### 🚀 Running locally

1. Install **Unity Hub** + **Unity 6** (with the WebGL Build Support module if you want to build for web)
2. Clone the repo:
   ```bash
   git clone https://github.com/AKN16/Jump-Run.git
   ```
3. Open the project with Unity Hub → select the cloned folder
4. Open the main scene in `Assets/Scenes` → press Play to try it in the Editor

#### Building for WebGL

1. `File > Build Profiles` → select the **Web** platform
2. `Player Settings > Publishing Settings` → set Compression Format to **Disabled**
3. Click **Build**, choose the `docs` folder to overwrite it (used for GitHub Pages)

### 🙏 Credits

Sprite/audio assets used in this game are credited in [`TaiNguyen/LICENSE & CREDITS.txt`](./TaiNguyen/LICENSE%20%26%20CREDITS.txt).

### 📄 License

*(Add your project's license here, e.g. MIT License, or leave blank if this is for learning/portfolio purposes only)*

---

## Tiếng Việt

Một game 2D platformer pixel-art được xây dựng bằng **Unity**. Người chơi điều khiển nhân vật vượt qua các chướng ngại vật (bẫy, sông, kẻ địch tuần tra), thu thập coin và chìa khóa để mở khóa chiến thắng.

**🔗 Chơi thử tại:** [https://akn16.github.io/Jump-Run/](https://akn16.github.io/Jump-Run/)

### 📸 Screenshots

*(Thêm ảnh chụp màn hình gameplay vào đây)*

### 🕹️ Gameplay

- **Di chuyển & nhảy** qua các nền tảng (platform), bao gồm cả nền tảng di chuyển (Moving Platform)
- **Thu thập Coin** rải rác trên map để ghi điểm
- **Tìm và lấy Key** để hoàn thành mục tiêu
- **Né tránh Trap** — chạm vào sẽ Game Over
- **Rơi xuống River** (sông) — Game Over ngay lập tức
- **Enemy tuần tra (Patrol)** theo quỹ đạo cố định; khi phát hiện Player đến gần sẽ **truy đuổi (Chase)**, và tự quay về vị trí ban đầu nếu đuổi quá xa
- **Camera bám theo Player**, giới hạn trong phạm vi bản đồ bằng Cinemachine Confiner 2D — không bị lộ ra ngoài map

### 🛠️ Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Engine | Unity 6 |
| Ngôn ngữ | C# |
| Camera | Cinemachine 3.x |
| Level design | Tilemap (Grid, Ground/ForeGround/BackGround, River) |
| Vật lý | Rigidbody2D, Tilemap Collider 2D, Composite Collider 2D |
| Nền tảng build | WebGL (host qua GitHub Pages) |

### 📁 Cấu trúc project

```
Assets/          → Scripts, Scenes, Prefabs, Animation...
ProjectSettings/ → Cấu hình project Unity (tags, layers, physics...)
Packages/        → Danh sách package Unity sử dụng
TaiNguyen/        → Tài nguyên gốc: sprite, âm thanh, font (kèm LICENSE & CREDITS)
docs/            → Bản build WebGL, dùng để host qua GitHub Pages
```

### 🚀 Chạy project trên máy

1. Cài **Unity Hub** + **Unity 6** (kèm module WebGL Build Support nếu muốn build web)
2. Clone repo:
   ```bash
   git clone https://github.com/AKN16/Jump-Run.git
   ```
3. Mở project bằng Unity Hub → chọn thư mục vừa clone
4. Mở scene chính trong `Assets/Scenes` → bấm Play để thử trong Editor

#### Build ra WebGL

1. `File > Build Profiles` → chọn platform **Web**
2. `Player Settings > Publishing Settings` → Compression Format = **Disabled**
3. Bấm **Build**, chọn thư mục `docs` để build đè lên (phục vụ GitHub Pages)

### 🙏 Credits

Tài nguyên sprite/âm thanh sử dụng trong game được ghi chú chi tiết tại [`TaiNguyen/LICENSE & CREDITS.txt`](./TaiNguyen/LICENSE%20%26%20CREDITS.txt).

### 📄 License

*(Thêm license cho project của bạn tại đây, ví dụ MIT License, hoặc để trống nếu chỉ dùng cho mục đích học tập/portfolio cá nhân)*
