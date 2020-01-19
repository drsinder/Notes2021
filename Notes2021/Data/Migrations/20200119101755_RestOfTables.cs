using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes2021.Data.Migrations
{
    public partial class RestOfTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserData",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    AuditID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(maxLength: 20, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false),
                    EventTime = table.Column<DateTime>(nullable: false),
                    Event = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.AuditID);
                });

            migrationBuilder.CreateTable(
                name: "HomePageMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(maxLength: 1000, nullable: false),
                    Posted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkedFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeFileId = table.Column<int>(nullable: false),
                    HomeFileName = table.Column<string>(maxLength: 20, nullable: false),
                    RemoteFileName = table.Column<string>(maxLength: 20, nullable: false),
                    RemoteBaseUri = table.Column<string>(maxLength: 450, nullable: false),
                    AcceptFrom = table.Column<bool>(nullable: false),
                    SendTo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(maxLength: 20, nullable: false),
                    EventTime = table.Column<DateTime>(nullable: false),
                    Event = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkQueue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedFileId = table.Column<int>(nullable: false),
                    LinkGuid = table.Column<string>(maxLength: 100, nullable: false),
                    Activity = table.Column<int>(nullable: false),
                    BaseUri = table.Column<string>(nullable: false),
                    Enqueued = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkQueue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberArchives = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(maxLength: 450, nullable: false),
                    NoteFileName = table.Column<string>(maxLength: 20, nullable: false),
                    NoteFileTitle = table.Column<string>(maxLength: 200, nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteFile_UserData_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserData",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SQLFile",
                columns: table => new
                {
                    FileId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(maxLength: 300, nullable: false),
                    ContentType = table.Column<string>(maxLength: 100, nullable: false),
                    Contributor = table.Column<string>(maxLength: 300, nullable: false),
                    Comments = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLFile", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    NoteFileId = table.Column<int>(nullable: false),
                    MarkOrdinal = table.Column<int>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: false),
                    NoteOrdinal = table.Column<int>(nullable: false),
                    NoteHeaderId = table.Column<long>(nullable: false),
                    ResponseOrdinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => new { x.UserId, x.NoteFileId, x.MarkOrdinal });
                    table.ForeignKey(
                        name: "FK_Mark_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteAccess",
                columns: table => new
                {
                    UserID = table.Column<string>(maxLength: 450, nullable: false),
                    NoteFileId = table.Column<int>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: false),
                    ReadAccess = table.Column<bool>(nullable: false),
                    Respond = table.Column<bool>(nullable: false),
                    Write = table.Column<bool>(nullable: false),
                    SetTag = table.Column<bool>(nullable: false),
                    DeleteEdit = table.Column<bool>(nullable: false),
                    ViewAccess = table.Column<bool>(nullable: false),
                    EditAccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteAccess", x => new { x.UserID, x.NoteFileId, x.ArchiveId });
                    table.ForeignKey(
                        name: "FK_NoteAccess_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteHeader",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteFileId = table.Column<int>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: false),
                    BaseNoteId = table.Column<long>(nullable: false),
                    NoteOrdinal = table.Column<int>(nullable: false),
                    ResponseOrdinal = table.Column<int>(nullable: false),
                    NoteSubject = table.Column<string>(maxLength: 200, nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    ThreadLastEdited = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ResponseCount = table.Column<int>(nullable: false),
                    AuthorID = table.Column<string>(maxLength: 450, nullable: true),
                    AuthorName = table.Column<string>(maxLength: 50, nullable: false),
                    LinkGuid = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteHeader_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Search",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    Option = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    NoteFileId = table.Column<int>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: false),
                    BaseOrdinal = table.Column<int>(nullable: false),
                    ResponseOrdinal = table.Column<int>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Search", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Search_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sequencer",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    NoteFileId = table.Column<int>(nullable: false),
                    Ordinal = table.Column<int>(nullable: false),
                    LastTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequencer", x => new { x.UserId, x.NoteFileId });
                    table.ForeignKey(
                        name: "FK_Sequencer_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteFileId = table.Column<int>(nullable: false),
                    SubscriberId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_NoteFile_NoteFileId",
                        column: x => x.NoteFileId,
                        principalTable: "NoteFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SQLFileContent",
                columns: table => new
                {
                    SQLFileId = table.Column<long>(nullable: false),
                    Content = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLFileContent", x => x.SQLFileId);
                    table.ForeignKey(
                        name: "FK_SQLFileContent_SQLFile_SQLFileId",
                        column: x => x.SQLFileId,
                        principalTable: "SQLFile",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteContent",
                columns: table => new
                {
                    NoteHeaderId = table.Column<long>(nullable: false),
                    NoteBody = table.Column<string>(maxLength: 100000, nullable: false),
                    DirectorMessage = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteContent", x => x.NoteHeaderId);
                    table.ForeignKey(
                        name: "FK_NoteContent_NoteHeader_NoteHeaderId",
                        column: x => x.NoteHeaderId,
                        principalTable: "NoteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    NoteHeaderId = table.Column<long>(nullable: false),
                    Tag = table.Column<string>(maxLength: 30, nullable: false),
                    NoteFileId = table.Column<int>(nullable: false),
                    ArchiveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => new { x.Tag, x.NoteHeaderId });
                    table.ForeignKey(
                        name: "FK_Tags_NoteHeader_NoteHeaderId",
                        column: x => x.NoteHeaderId,
                        principalTable: "NoteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mark_NoteFileId",
                table: "Mark",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_UserId_NoteFileId",
                table: "Mark",
                columns: new[] { "UserId", "NoteFileId" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteAccess_NoteFileId",
                table: "NoteAccess",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteFile_OwnerId",
                table: "NoteFile",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteHeader_LinkGuid",
                table: "NoteHeader",
                column: "LinkGuid");

            migrationBuilder.CreateIndex(
                name: "IX_NoteHeader_NoteFileId",
                table: "NoteHeader",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteHeader_NoteFileId_ArchiveId",
                table: "NoteHeader",
                columns: new[] { "NoteFileId", "ArchiveId" });

            migrationBuilder.CreateIndex(
                name: "IX_Search_NoteFileId",
                table: "Search",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Sequencer_NoteFileId",
                table: "Sequencer",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_NoteFileId",
                table: "Subscription",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_NoteFileId",
                table: "Tags",
                column: "NoteFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_NoteHeaderId",
                table: "Tags",
                column: "NoteHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_NoteFileId_ArchiveId",
                table: "Tags",
                columns: new[] { "NoteFileId", "ArchiveId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "HomePageMessage");

            migrationBuilder.DropTable(
                name: "LinkedFile");

            migrationBuilder.DropTable(
                name: "LinkLog");

            migrationBuilder.DropTable(
                name: "LinkQueue");

            migrationBuilder.DropTable(
                name: "Mark");

            migrationBuilder.DropTable(
                name: "NoteAccess");

            migrationBuilder.DropTable(
                name: "NoteContent");

            migrationBuilder.DropTable(
                name: "Search");

            migrationBuilder.DropTable(
                name: "Sequencer");

            migrationBuilder.DropTable(
                name: "SQLFileContent");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SQLFile");

            migrationBuilder.DropTable(
                name: "NoteHeader");

            migrationBuilder.DropTable(
                name: "NoteFile");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
