# 🎮 Jump-Run

Một game 2D platformer pixel-art được xây dựng bằng **Unity**. Người chơi điều khiển nhân vật vượt qua các chướng ngại vật (bẫy, sông, kẻ địch tuần tra), thu thập coin và chìa khóa để mở khóa chiến thắng.

**🔗 Chơi thử tại:** [https://akn16.github.io/Jump-Run/](https://akn16.github.io/Jump-Run/)

---

## 📸 Screenshots

*(Thêm ảnh chụp màn hình gameplay vào đây)*

---

## 🕹️ Gameplay

- **Di chuyển & nhảy** qua các nền tảng (platform), bao gồm cả nền tảng di chuyển (Moving Platform)
- **Thu thập Coin** rải rác trên map để ghi điểm
- **Tìm và lấy Key** để hoàn thành mục tiêu
- **Né tránh Trap** — chạm vào sẽ Game Over
- **Rơi xuống River** (sông) — Game Over ngay lập tức
- **Enemy tuần tra (Patrol)** theo quỹ đạo cố định; khi phát hiện Player đến gần sẽ **truy đuổi (Chase)**, và tự quay về vị trí ban đầu nếu đuổi quá xa
- **Camera bám theo Player**, giới hạn trong phạm vi bản đồ bằng Cinemachine Confiner 2D — không bị lộ ra ngoài map

---

## 🛠️ Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Engine | Unity 6 |
| Ngôn ngữ | C# |
| Camera | Cinemachine 3.x |
| Level design | Tilemap (Grid, Ground/ForeGround/BackGround, River) |
| Vật lý | Rigidbody2D, Tilemap Collider 2D, Composite Collider 2D |
| Nền tảng build | WebGL (host qua GitHub Pages) |

---

## 📁 Cấu trúc project

```
Assets/          → Scripts, Scenes, Prefabs, Animation...
ProjectSettings/ → Cấu hình project Unity (tags, layers, physics...)
Packages/        → Danh sách package Unity sử dụng
TaiNguyen/        → Tài nguyên gốc: sprite, âm thanh, font (kèm LICENSE & CREDITS)
docs/            → Bản build WebGL, dùng để host qua GitHub Pages
```

---

## 🚀 Chạy project trên máy

1. Cài **Unity Hub** + **Unity 6** (kèm module WebGL Build Support nếu muốn build web)
2. Clone repo:
   ```bash
   git clone https://github.com/AKN16/Jump-Run.git
   ```
3. Mở project bằng Unity Hub → chọn thư mục vừa clone
4. Mở scene chính trong `Assets/Scenes` → bấm Play để thử trong Editor

### Build ra WebGL

1. `File > Build Profiles` → chọn platform **Web**
2. `Player Settings > Publishing Settings` → Compression Format = **Disabled**
3. Bấm **Build**, chọn thư mục `docs` để build đè lên (phục vụ GitHub Pages)

---

## 🙏 Credits

Tài nguyên sprite/âm thanh sử dụng trong game được ghi chú chi tiết tại [`TaiNguyen/LICENSE & CREDITS.txt`](./TaiNguyen/LICENSE%20%26%20CREDITS.txt).

---

## 📄 License

*(Thêm license cho project của bạn tại đây, ví dụ MIT License, hoặc để trống nếu chỉ dùng cho mục đích học tập/portfolio cá nhân)*
