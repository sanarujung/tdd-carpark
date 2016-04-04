using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CarPark.Models;

namespace CarPark.Facts
{
    public class TicketFacts
    {
        public class General
        {

            [Fact]
            public void BasicUsage()
            {
                // arrange

                Ticket t = new Ticket();
                t.PlateNo = "1707";
                t.DateIn = new DateTime(2016, 1, 1, 9, 0, 0); // 9am
                t.DateOut = DateTime.Parse("13:30"); // 1:30 pm

                // act


                // assert
                Assert.Equal("1707", t.PlateNo);
                Assert.Equal(9, t.DateIn.Hour);
                Assert.Equal(13, t.DateOut.Value.Hour);
            }

            [Fact]
            public void NewTicket_HasNoDateOut()
            {
                var t = new Ticket();
                Assert.Null(t.DateOut);
            }

        }

        public class ParkingFeeProperty
        {
            [Fact]
            public void NewTicket_DontKnowparkingFee()
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = null;

                Assert.Null(t.ParkingFee);

            }


            [Fact]
            public void First15Minutes_Free()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(0m, fee);
            }

            [Fact]
            public void WithInFirst3Hours_50Bath()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15:01");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(50m, fee);
            }


            [Fact]
            public void WithInFirst3HoursII_50Bath()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("12:11");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(50m, fee);
            }


            [Fact]
            public void For4Hours_80Bath()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("13:00");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(80m, fee);
            }

            [Fact(Skip = "bug")]
            public void For6Hours_140Bath()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("10:00");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(140m, fee);
            }

            [Fact]
            public void For6HoursExceed15Minutes_GetExtraHour()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("15:15:01");

                //act
                var fee = t.ParkingFee;

                //assert 
                Assert.Equal(170m, fee);
            }

            [Fact]
            public void DateOutInbeforeDateIn_ThrowsExeption()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("10:00");
                t.DateOut = DateTime.Parse("09:11");

                var ex = Assert.Throws<Exception>(() =>
                {
                    //act
                    var fee = t.ParkingFee;
                });

                Assert.Equal("Invalid date", ex.Message);
            }

            [Theory]
            [InlineData("9:00","15:00",140)]
            [InlineData("9:00","16:00",170)]
            [InlineData("9:00","17:00",200)]
            [InlineData("9:00","18:00",230)]
            [InlineData("9:00","19:00", 260)]
            public void SamplingTests(string dateIn, string dateOut, decimal expectedFee)
            {
                var t = new Ticket();
                t.DateIn = DateTime.Parse(dateIn);
                t.DateOut = DateTime.Parse(dateOut);

                var fee = t.ParkingFee;

                Assert.Equal(expectedFee, fee);
            }
        }

    }
}
