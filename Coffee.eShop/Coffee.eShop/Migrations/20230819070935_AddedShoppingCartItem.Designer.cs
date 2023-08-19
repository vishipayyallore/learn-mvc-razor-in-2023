﻿// <auto-generated />
using System;
using Coffee.eShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coffee.eShop.Migrations
{
    [DbContext(typeof(CoffeeShopDbContext))]
    [Migration("20230819070935_AddedShoppingCartItem")]
    partial class AddedShoppingCartItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Coffee.eShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrendingProduct")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Detail = "The Americano coffee is a classic espresso-based drink that is simple yet satisfying. It is made by adding hot water to a shot of espresso, which dilutes the intensity and results in a rich, bold coffee with a smooth finish. This versatile drink can be enjoyed on its own or with a splash of cream, making it a popular choice for coffee lovers everywhere. Whether you need a pick-me-up in the morning or a midday boost, the Americano is a dependable choice that never fails to deliver.",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704066/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/americano_gorkrx.png",
                            IsTrendingProduct = false,
                            Name = "Americano",
                            Price = 25m
                        },
                        new
                        {
                            Id = 2,
                            Detail = "Cortado coffee is a traditional Spanish coffee beverage that has gained popularity worldwide. It is a smooth and creamy coffee that combines equal parts of espresso and warm milk, creating a perfect balance of intense coffee flavor and rich creaminess. This coffee is perfect for coffee lovers who want a bit of sweetness in their coffee without sacrificing its robust flavor. Cortado coffee is made using high-quality espresso beans, freshly brewed and combined with steamed milk to create a velvety, smooth and flavorful coffee. Whether you are a coffee aficionado or a coffee lover, Cortado coffee is the perfect coffee to start your day with or to enjoy in the afternoon. Try our Cortado coffee today and experience the unique and satisfying taste of this traditional Spanish coffee beverage.",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704067/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/cortado_rs5lwa.png",
                            IsTrendingProduct = true,
                            Name = "Cortado",
                            Price = 25m
                        },
                        new
                        {
                            Id = 3,
                            Detail = "Mocha coffee is a rich and creamy blend that combines the bold flavor of coffee with the sweetness of chocolate. This delectable drink is perfect for those who love the taste of chocolate but also enjoy a good coffee. Mocha coffee is made with a shot of espresso, steamed milk, and chocolate syrup. The result is a smooth and creamy drink with a sweet, chocolatey taste and a strong coffee finish. Whether you are looking for a sweet and indulgent treat or a pick-me-up in the morning, Mocha coffee is sure to satisfy your cravings. Get your fix of this delicious drink today and experience the perfect marriage of coffee and chocolate!",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704066/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/mocha_a80hlu.png",
                            IsTrendingProduct = false,
                            Name = "Mocha",
                            Price = 22m
                        },
                        new
                        {
                            Id = 4,
                            Detail = "Macchiato Coffee is a classic espresso-based beverage with a rich, creamy flavor and a smooth, velvety texture. Made from high-quality, hand-selected coffee beans, this drink is the perfect pick-me-up for coffee lovers who want a strong, bold taste without the bitterness or harshness of traditional espresso. With a layer of dense, frothed milk that sits on top of the shot of espresso, the Macchiato Coffee is the perfect balance of bold coffee flavor and creamy sweetness. Whether you're a busy professional in need of a morning boost, or an espresso aficionado seeking a more refined coffee experience, the Macchiato Coffee is the ultimate choice for anyone who loves coffee. So why wait? Visit our online coffee store today and try a Macchiato Coffee today!",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704064/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/macchiato_jelmpv.png",
                            IsTrendingProduct = true,
                            Name = "Macchiato",
                            Price = 15m
                        },
                        new
                        {
                            Id = 5,
                            Detail = "Flat White Coffee is a classic espresso-based beverage that is a staple in coffee shops all over the world. This smooth and creamy coffee is made with perfectly pulled shots of espresso, topped with velvety steamed milk and a thin layer of microfoam. This gives the Flat White its signature smooth and creamy texture and a rich, coffee flavor that is balanced by the sweetness of the milk. The Flat White is a perfect coffee for those who love a strong coffee taste with a hint of sweetness. Whether you are a coffee aficionado or just looking for a delicious coffee to start your day, a Flat White from our store is the perfect choice!",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704064/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/flat-white_icl8cr.png",
                            IsTrendingProduct = false,
                            Name = "Flat White",
                            Price = 18m
                        },
                        new
                        {
                            Id = 6,
                            Detail = "Decaf Coffee, also known as decaffeinated coffee, is a coffee beverage that has had the majority of its caffeine content removed. It is a perfect option for those who enjoy the taste and aroma of coffee, but want to avoid the stimulating effects of caffeine. Decaf coffee is made from 100% Arabica beans, which are carefully roasted to bring out their natural sweetness and rich flavor. Whether you're a coffee aficionado or just looking for a comforting cup of joe, decaf coffee is the perfect choice. It offers all the delicious taste of regular coffee, without the caffeine jitters, making it an ideal choice for late-night sipping, morning or afternoon pick-me-ups, or simply whenever you want to relax and enjoy a cup of coffee. So why wait? Treat yourself to a delicious cup of decaf coffee today and enjoy the taste of coffee, without the caffeine.",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704069/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/decaf-coffee_p3bth2.png",
                            IsTrendingProduct = false,
                            Name = "Decaf",
                            Price = 25m
                        },
                        new
                        {
                            Id = 7,
                            Detail = "Irish coffee is a warm, comforting drink that combines the bold flavor of coffee with the smooth sweetness of Irish whiskey and a touch of cream. Our version of this classic cocktail is made with rich, bold coffee and the finest Irish whiskey for a perfect balance of flavors. The cream is gently whipped to a smooth consistency and poured over the coffee, creating a luxurious, creamy layer that balances the whiskey’s warmth. Whether you’re looking for a cozy drink on a cold day or a fun nightcap after a night out, Irish coffee is a perfect choice. Order yours today and experience the perfect blend of coffee and whiskey.",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704079/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/Irish_Coffee_ncjb0t.png",
                            IsTrendingProduct = true,
                            Name = "Irish Coffee",
                            Price = 15m
                        },
                        new
                        {
                            Id = 8,
                            Detail = "Iced coffee is a refreshing and delicious way to enjoy your coffee, perfect for hot summer days or for anyone looking for a cool pick-me-up. Our iced coffee is made with high-quality, freshly brewed coffee, which is then chilled and served over ice. We use only the finest coffee beans, expertly roasted to bring out their rich, full-bodied flavor, ensuring that every sip is a treat. Our iced coffee is available in a variety of flavors, including classic black, vanilla, caramel, and mocha, making it the perfect choice for coffee lovers of all tastes. So why wait? Treat yourself to a cold, refreshing glass of iced coffee today!",
                            ImageUrl = "https://res.cloudinary.com/durcypdqc/image/upload/v1675704079/Coffee%20Shop%20Asp%20.NET%20Core%20Assets/Iced_Coffee_o2nenz.png",
                            IsTrendingProduct = false,
                            Name = "Iced Coffee",
                            Price = 13m
                        });
                });

            modelBuilder.Entity("Coffee.eShop.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("Coffee.eShop.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("Coffee.eShop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
