using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrillFusion_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbWithImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1553979459-d2229ba7433b?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1594212699903-ec8a3eca50f5?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1520072959219-c595dc870360?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1558030006-450675393462?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1544025162-d76694265947?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1598515214211-89d3c73ae83b?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1611489142329-7f31aa425f47?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1573080496219-bb080dd4f877?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1639024471283-03518883512d?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1543339494-b4cd4f7ba686?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1551754655-cd27e38d2076?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1546173159-315724a31696?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1572490122747-3968b75cc699?w=400" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Image" },
                values: new object[] { "...", "https://images.unsplash.com/photo-1523677011781-c91d1bbe2f9e?w=400" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Juicy smashed beef patty with cheddar cheese, lettuce, tomato, and our signature sauce on a brioche bun.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Two flame-grilled patties with pepper jack cheese, jalapeños, crispy onions, and chipotle mayo.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Thick beef patty topped with smoky bacon, caramelized onions, BBQ sauce, and aged cheddar.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Grilled beef patty with sautéed mushrooms, Swiss cheese, garlic aioli, and arugula.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Image" },
                values: new object[] { "12oz prime ribeye grilled to perfection, served with roasted garlic butter and seasonal vegetables.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Slow-cooked pork ribs glazed with our house BBQ sauce, served with coleslaw and cornbread.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Marinated half chicken grilled over open flame, served with mashed potatoes and grilled corn.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Four rosemary-marinated lamb chops grilled to medium, served with mint jelly and roasted potatoes.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Crispy golden fries topped with melted cheese, bacon bits, sour cream, and chives.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Hand-battered thick-cut onion rings fried to a golden crisp, served with smoky dipping sauce.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Creamy three-cheese macaroni baked with a golden breadcrumb crust.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Charred sweet corn on the cob brushed with herb butter and a sprinkle of smoked paprika.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Image" },
                values: new object[] { "Freshly squeezed lemonade blended with ripe mango purée, served over ice.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Image" },
                values: new object[] { "A thick vanilla milkshake with a hint of caramel and salted pretzel crumble on top.", "" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Image" },
                values: new object[] { "House-made lemonade with fresh mint, cucumber slices, and a splash of sparkling water.", "" });
        }
    }
}
