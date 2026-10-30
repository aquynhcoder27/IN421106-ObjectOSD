# Domain bài toán EduHub

## 1. Vai trò: Sinh viên

### US-01: Sinh viên rời khỏi một CLB

- Đã là thành viên CLB ➜ rời CLB ➜ xác nhận ➜ xóa tên trong `club.Members`
- Chưa là thành viên ➜ xin rời CLB ➜ bị từ chối (chưa tham gia)

### US-02: Sinh viên tìm kiếm sự kiện

- Tìm theo tên sự kiện ➜ có sự kiện hợp lệ ➜ hiển thị thông tin sự kiện qua `GetSuKien()`;
- Tìm theo tên sự kiện ➜ không có sự kiện hợp lệ ➜ báo không có sự kiện cần tìm

### US-03: Sinh viên hủy tham gia một sự kiện

- Đã đăng ký tham gia sự kiện ➜ đăng ký hủy tham gia ➜ sự kiện chưa bắt đầu ➜ chuyển sang trạng thái `Cancelled` ➜ xóa tên trong `event.Members`
- Đã đăng ký tham gia sự kiện ➜ đăng ký hủy tham gia ➜ sự kiện đã bắt đầu ➜ báo sự kiện đã diễn ra

## 2. Vai trò: Ban CLB

### US-01: Ban CLB hủy sự kiện

- Sự kiện chưa được tạo ➜ hủy sự kiện ➜ báo lỗi
- Sự kiện đã được tạo ➜ hủy sự kiện ➜ kiểm tra quyền ➜ xác nhận ➜ hủy thành công

### US-02: Ban CLB xuất danh sách tham gia

- Chọn sự kiện cần xuất ➜ có thành viên tham gia ➜ xuất danh sách chứa dữ liệu đầy đủ
- Chọn sự kiện cần xuất ➜ chưa có thành viên tham gia ➜ xuất danh sách chỉ có tiêu đề cột

## 3. Vai trò: Admin

### US-01: Admin sửa thông tin một thành viên

- Chọn ô thông tin thành viên cần sửa ➜ có quyền sửa ➜ nhập thông tin đúng định dạng ➜ thay đổi thông tin
- Chọn ô thông tin thành viên cần sửa ➜ không có quyền sửa ➜ báo lỗi
