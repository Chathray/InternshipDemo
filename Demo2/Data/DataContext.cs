using Microsoft.EntityFrameworkCore;

namespace WebApplication
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValue("success");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasDefaultValue('0');

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasDefaultValue("../img/img1.jpg");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasDefaultValue("0");
            });

            modelBuilder.Entity<Intern>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnType("varchar(23)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasDefaultValue("../img/img1.jpg");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasDefaultValue("0");

                entity.Property(e => e.Organization)
                    .IsRequired()
                    .HasDefaultValue("Quy Nhon University");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasDefaultValue("TIP C#");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasColumnType("nvarchar(200)");

                entity.Property(e => e.InData)
                    .IsRequired()
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.OutData)
                   .IsRequired()
                   .HasColumnType("nvarchar(MAX)");
            });


            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User
                {
                    ID=1,
                    FirstName = "Admin",
                    LastName = "One",
                    Email = "admin@x",
                    PasswordHash = "$2a$11$cP33dCpCwF6KKK5wzqcQGOjBEwfFjXRK.ZofYLw1lVtX5kr5nVZ2q"
                },
                 new User
                {
                    ID=2,
                    FirstName = "Admin",
                    LastName = "Two",
                    Email = "admin@xx",
                    PasswordHash = "$2a$11$cP33dCpCwF6KKK5wzqcQGOjBEwfFjXRK.ZofYLw1lVtX5kr5nVZ2q"
                },
                  new User
                {
                    ID=3,
                    FirstName = "Admin",
                    LastName = "Three",
                    Email = "admin@xxx",
                    PasswordHash = "$2a$11$cP33dCpCwF6KKK5wzqcQGOjBEwfFjXRK.ZofYLw1lVtX5kr5nVZ2q"
                }
            });

            modelBuilder.Entity<EventType>().HasData(new EventType[]
            {
                new EventType
                {
                    Type = "Personal",
                    Color = "primary",
                    ClassName = "fullcalendar-custom-event-hs-team"
                }, new EventType
                {
                    Type = "Reminders",
                    Color = "danger",
                    ClassName = "fullcalendar-custom-event-reminders"

                }, new EventType
                {
                    Type = "Tasks",
                    Color = "dark",
                    ClassName = "fullcalendar-custom-event-tasks"

                },new EventType
                {
                    Type = "Holidays",
                    Color = "warning",
                    ClassName = "fullcalendar-custom-event-holidays"
                },
            });

            modelBuilder.Entity<Question>().HasData(new Question[]
            {
                new Question
                {
                    ID= 1,
                    Group = "Recruit",
                    InData = "Cho em hỏi TMA phỏng vấn tiếng Anh hay tiếng Việt?",
                    OutData = "Bên mình sẽ phỏng vấn cả tiếng Anh và tiếng Việt nha bạn."
                },
                new Question
                {
                    ID= 2,
                    Group = "Recruit",
                    InData = "Anh/chị cho em hỏi nếu test tiếng Anh ở công ty thì đề thi như thế nào ạ? Bao lâu sẽ có kết quả?",
                    OutData = "Chào bạn, đề thi sẽ theo chuẩn đề TOEIC, thông thường kết quả sẽ được thông báo trong vòng 1 tuần, tùy thuộc vào mỗi dự án."
                },
                new Question
                {
                    ID= 3,
                    Group = "Recruit",
                    InData = "Em là SV năm cuối (đang làm luận văn), chuyên ngành: Điện tử - Viễn thông, trường: ĐH Bách Khoa Tp. HCM. Em có 2 câu hỏi: TMA có tuyển thực tập sinh không? Nếu có, yêu cầu cụ thể (GPA, Tiếng Anh,...) như thế nào?",
                    OutData = "TMA thường xuyên tuyển thực tập sinh, sắp tới bên chị sẽ nhận hồ sơ thực tập để chuẩn bị cho đợt thực tập kế tiếp vào tháng 9. Hồ sơ ứng tuyển bao gồm:<ul class='mt-3'><li>CV Tiếng Anh;</li><li> Bảng điểm hoặc bằng Tiếng Anh(nếu có);</li><li> Hình 3x4;</li><li> Giấy giới thiệu thực tập</li></ul> "
                },
                new Question
                {
                    ID= 4,
                    Group = "Internship",
                    InData = "Ngoại ngữ có yêu cầu cao không Ad?",
                    OutData = "Nếu em đã có các bằng như (TOEIC, TOEFL, IELTS) tương đương TOEIC 450 trở lên thì không phải làm bài test em nhé."
                },
                new Question
                {
                    ID= 5,
                    Group = "Internship",
                    InData = "Test thực tập đầu vào như thế nào chị?",
                    OutData = "Bài test đầu vào gồm: IQ (25’) và tiếng Anh (nếu em chưa có bằng)"
                },
                new Question
                {
                    ID= 6,
                    Group = "Internship",
                    InData = "Thời gian thực tập yêu cầu là bao nhiêu vậy Ad?",
                    OutData = "Thời gian thực tập kéo dài 3 tháng, 2.5 ngày/tuần em à."
                },
            });

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Intern>().ToTable("Interns");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<EventType>().ToTable("EventTypes");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Intern> Interns { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}