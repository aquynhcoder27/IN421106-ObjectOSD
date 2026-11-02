# Bài tập Chương 2 — Refactoring EduHub

Solution bắt đầu từ starter Ch02 và được refactor theo SOLID. Phân tích vi phạm ban đầu nằm ở [`Ch02.Refactor/docs/solid-analysis.md`](Ch02.Refactor/docs/solid-analysis.md).

## Chạy

Từ thư mục gốc repository:

```bash
dotnet build BaiTap/Chuong02/Ch02.Refactor.sln
dotnet test BaiTap/Chuong02/Ch02.Refactor.sln
dotnet run --project BaiTap/Chuong02/Ch02.Refactor
```

`Program.cs` là composition root: nơi ghép các implementation cụ thể. Demo thể hiện Factory, Decorator, Observer, hai Strategy báo cáo và luật kiểm tra Domain.
