using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GrillFusion_API.Migrations
{
    /// <inheritdoc />
    public partial class seedMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SpecialTag" },
                values: new object[,]
                {
                    { 1, "Burger", "Juicy smashed beef patty with cheddar cheese, lettuce, tomato, and our signature sauce on a brioche bun.", "", "Classic Smash Burger", 12.99, "Best Seller" },
                    { 2, "Burger", "Two flame-grilled patties with pepper jack cheese, jalapeños, crispy onions, and chipotle mayo.", "", "Double Inferno Burger", 15.99, "Spicy" },
                    { 3, "Burger", "Thick beef patty topped with smoky bacon, caramelized onions, BBQ sauce, and aged cheddar.", "", "BBQ Bacon Burger", 14.49, "" },
                    { 4, "Burger", "Grilled beef patty with sautéed mushrooms, Swiss cheese, garlic aioli, and arugula.", "", "Mushroom Swiss Burger", 13.99, "" },
                    { 5, "Grill", "12oz prime ribeye grilled to perfection, served with roasted garlic butter and seasonal vegetables.", "", "Ribeye Steak", 34.990000000000002, "Chef's Choice" },
                    { 6, "Grill", "Slow-cooked pork ribs glazed with our house BBQ sauce, served with coleslaw and cornbread.", "", "BBQ Ribs Half Rack", 24.989999999999998, "Best Seller" },
                    { 7, "Grill", "Marinated half chicken grilled over open flame, served with mashed potatoes and grilled corn.", "", "Grilled Chicken Platter", 18.989999999999998, "" },
                    { 8, "Grill", "Four rosemary-marinated lamb chops grilled to medium, served with mint jelly and roasted potatoes.", "", "Lamb Chops", 29.989999999999998, "New" },
                    { 9, "Sides", "Crispy golden fries topped with melted cheese, bacon bits, sour cream, and chives.", "", "Loaded Fries", 7.9900000000000002, "Best Seller" },
                    { 10, "Sides", "Hand-battered thick-cut onion rings fried to a golden crisp, served with smoky dipping sauce.", "", "Onion Rings", 5.9900000000000002, "" },
                    { 11, "Sides", "Creamy three-cheese macaroni baked with a golden breadcrumb crust.", "", "Mac & Cheese", 6.4900000000000002, "" },
                    { 12, "Sides", "Charred sweet corn on the cob brushed with herb butter and a sprinkle of smoked paprika.", "", "Grilled Corn", 4.4900000000000002, "" },
                    { 13, "Drinks", "Freshly squeezed lemonade blended with ripe mango purée, served over ice.", "", "Mango Lemonade", 4.9900000000000002, "New" },
                    { 14, "Drinks", "A thick vanilla milkshake with a hint of caramel and salted pretzel crumble on top.", "", "Smoky BBQ Shake", 6.9900000000000002, "" },
                    { 15, "Drinks", "House-made lemonade with fresh mint, cucumber slices, and a splash of sparkling water.", "", "Iced Craft Lemonade", 3.9900000000000002, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
