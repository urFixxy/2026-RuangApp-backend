using RuangApp.Api.Models;

namespace RuangApp.Api.Data;

public static class Seeder
{
    public static void SeedData(RuangAppContext context)
    {
        if (context.Rooms.Any())
        {
            return;
        }

        // Seed Rooms
        var rooms = new List<Rooms>
        {
            new Rooms
            {
                roomName = "Ruang Meeting A",
                capacity = 20,
                location = "Lantai 1",
                isAvailable = true
            },
            new Rooms
            {
                roomName = "Ruang Meeting B",
                capacity = 15,
                location = "Lantai 2",
                isAvailable = true
            },
            new Rooms
            {
                roomName = "Ruang Konferensi",
                capacity = 50,
                location = "Lantai 3",
                isAvailable = true
            },
            new Rooms
            {
                roomName = "Ruang Training",
                capacity = 30,
                location = "Lantai 2",
                isAvailable = true
            }
        };

        context.Rooms.AddRange(rooms);
        context.SaveChanges();

        // Seed Borrowings
        var borrowings = new List<Borrowing>
        {
            new Borrowing
            {
                RoomId = rooms[0].Id,
                borrowerName = "Budi Santoso",
                borrowingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                startTime = new TimeOnly(08, 00),
                endTime = new TimeOnly(10, 00),
                purpose = "Meeting tim development",
                status = "Selesai"
            },
            new Borrowing
            {
                RoomId = rooms[1].Id,
                borrowerName = "Siti Nurhaliza",
                borrowingDate = DateOnly.FromDateTime(DateTime.Now),
                startTime = new TimeOnly(13, 00),
                endTime = new TimeOnly(14, 30),
                purpose = "Presentasi klien",
                status = "Aktif"
            },
            new Borrowing
            {
                RoomId = rooms[2].Id,
                borrowerName = "Ahmad Hidayat",
                borrowingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                startTime = new TimeOnly(10, 00),
                endTime = new TimeOnly(12, 00),
                purpose = "Rapat strategis",
                status = "Pending"
            },
            new Borrowing
            {
                RoomId = rooms[3].Id,
                borrowerName = "Rina Wijaya",
                borrowingDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                startTime = new TimeOnly(09, 00),
                endTime = new TimeOnly(11, 30),
                purpose = "Training internal",
                status = "Aktif"
            }
        };

        context.Borrowings.AddRange(borrowings);
        context.SaveChanges();
    }
}
