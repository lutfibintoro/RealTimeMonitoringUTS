using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeMonitoringUTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnTableSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Temperature",
                schema: "dbo",
                table: "Sensor",
                newName: "TemperatureC");

            migrationBuilder.AlterColumn<decimal>(
                name: "MethaneGas",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "HydrogenGas",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Humidity",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureC",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "AlcohonGas",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LpgGas",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Smoke",
                schema: "dbo",
                table: "Sensor",
                type: "DECIMAL(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "X",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Z",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcohonGas",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "LpgGas",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Smoke",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "X",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Y",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Z",
                schema: "dbo",
                table: "Sensor");

            migrationBuilder.RenameColumn(
                name: "TemperatureC",
                schema: "dbo",
                table: "Sensor",
                newName: "Temperature");

            migrationBuilder.AlterColumn<int>(
                name: "MethaneGas",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "HydrogenGas",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Humidity",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Temperature",
                schema: "dbo",
                table: "Sensor",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");
        }
    }
}
