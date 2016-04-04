﻿using System;
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

                Ticket t;
                t = new Ticket();
                t.PlateNo = "1707";
                t.DateIn = new DateTime(2016,1,1,9,0,0); // 9am
                t.DateOut = DateTime.Parse("13:30"); // 1:30 pm

                // act


                // assert
                //Assert.Equal("1707", t.PlateNo);
                //Assert.Equal(9, t.DateIn.Hour);
                //Assert.Equal(13, t.DateOut);
            }

        }

        public class ParkingFeeProperty
        {
            [Fact]
            public void First15Minutes_Free()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15");

                //act
                decimal fee = t.ParkingFee;

                //assert 
                Assert.Equal(0m,fee);
            }

            [Fact]
            public void WithInFirst3Hours_50Bath()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15:01");

                //act
                decimal fee = t.ParkingFee;

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
                decimal fee = t.ParkingFee;

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
                decimal fee = t.ParkingFee;

                //assert 
                Assert.Equal(80m, fee);
            }

            //[Fact]
            //public void For6Hours_140Bath()
            //{
            //    //arrange
            //    var t = new Ticket();
            //    t.DateIn = DateTime.Parse("9:00");
            //    t.DateOut = DateTime.Parse("14:00");

            //    //act
            //    decimal fee = t.ParkingFee;

            //    //assert 
            //    Assert.Equal(140m, fee);
            //}

            [Fact]
            public void For6HoursExceed15Minutes_GetExtraHour()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("15:15:01");

                //act
                decimal fee = t.ParkingFee;

                //assert 
                Assert.Equal(170m, fee);
            }

            [Fact]
            public void test()
            {
                //arrange
                var t = new Ticket();
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("14:11");
               
                //act
                decimal fee = t.ParkingFee;

                //assert 
                Assert.Equal(110m, fee);
            }
        }

    }
}
