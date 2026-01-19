using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSeedToStudentCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 1, 1 },
                column: "EnrolledAt",
                value: new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 2, 1 },
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 3, 2 },
                column: "EnrolledAt",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 1, 1 },
                column: "EnrolledAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 2, 1 },
                column: "EnrolledAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 3, 2 },
                column: "EnrolledAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
