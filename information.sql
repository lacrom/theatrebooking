select * from seat 

--update seat set Information = N'Без ограничения видимости' where ID between 1 and 385
--update seat set Information = N'Без ограничения видимости' where ID between 389 and 443

--update seat set Information = N'Без ограничения видимости' where AreaDescription like N'%Бенуар%' and SeatNumber in(N'1', N'3', N'5')
--update seat set Information = N'Без ограничения видимости' where AreaDescription like N'%Амфитеатр%' and RowNumber = 1
--update seat set Information = N'Без ограничения видимости' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(N'1', N'3', N'5') and RowNumber in (N'10', N'11', N'12', N'13', N'14', N'15')

--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%Партер%' and RowNumber = 17
--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%Бенуар%' and SeatNumber in(N'2', N'4', N'6')
--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%Амфитеатр%' and RowNumber in (N'2', N'3')
--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(N'1', N'3', N'5') and RowNumber in (N'1', N'2', N'3', N'4', N'5', N'6', N'7', N'8', N'9')

--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%1 ярус%' and SeatNumber in(N'1', N'3', N'5') and RowNumber in (N'10', N'11')
--update seat set Information = N'Возможное ограничение видимости до 10%' where AreaDescription like N'%1 ярус%' and SeatNumber in (1,2,3,4,5,6,7) and RowNumber = 1 and RowName like N'%Ложа 12%'

--update seat set Information = N'Возможное ограничение видимости до 20%' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(2,4,6) and RowNumber in (10,11,12,13,14,15)
--update seat set Information = N'Возможное ограничение видимости до 20%' where AreaDescription like N'%1 ярус%' and SeatNumber in(1,3,5) and RowNumber between 1 and 9
--update seat set Information = N'Возможное ограничение видимости до 20%' where AreaDescription like N'%1 ярус%' and SeatNumber = 8 and RowNumber = 1 and RowName like N'%Ложа 12%'
--update seat set Information = N'Возможное ограничение видимости до 20%' where AreaDescription like N'%Балкон 2 яруса%' and SeatNumber between 5 and 12 and RowNumber = 1
--update seat set Information = N'Возможное ограничение видимости до 20%' where AreaDescription like N'%1 ярус,%' and SeatNumber in(1,2,3,5,7) and RowNumber = 1 and RowName = N'Ложа'

--update seat set Information = N'Возможное ограничение видимости до 25%' where AreaDescription like N'%1 ярус%' and SeatNumber in (1,2,3,4,5,6,7) and RowNumber = 2 and RowName like N'%Ложа 12%'
--update seat set Information = N'Возможное ограничение видимости до 25%' where AreaDescription like N'%1 ярус%' and SeatNumber in(2,4,6) and RowNumber in (10,11)
--update seat set Information = N'Возможное ограничение видимости до 25%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 1 and SeatNumber in (1,2,3,4)
--update seat set Information = N'Возможное ограничение видимости до 25%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 2 and SeatNumber between 1 and 11
--update seat set Information = N'Возможное ограничение видимости до 25%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 1 and SeatNumber between 5 and 37

--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%1 ярус%' and SeatNumber = 8 and RowNumber = 2 and RowName like N'%Ложа 12%'
--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 2 and SeatNumber = 12
--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 3 and SeatNumber in (9,10)
--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%3 ярус,%'
--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 1 and (SeatNumber between 38 and 41 or SeatNumber between 1 and 4)
--update seat set Information = N'Возможное ограничение видимости до 30%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 2 and SeatNumber between 6 and 36

--update seat set Information = N'Возможное ограничение видимости до 35%' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(N'2', N'4', N'6') and RowNumber in (N'1', N'2', N'3', N'4', N'5', N'6', N'7', N'8', N'9')
--update seat set Information = N'Возможное ограничение видимости до 35%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 3 and SeatNumber between 5 and 8
--update seat set Information = N'Возможное ограничение видимости до 35%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber = N'1' and SeatNumber between 31 and 52

--update seat set Information = N'Возможное ограничение видимости до 40%' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(N'7', N'8') and RowNumber between 10 and 15
--update seat set Information = N'Возможное ограничение видимости до 40%' where AreaDescription like N'%1 ярус,%' and SeatNumber in(N'7', N'8') and RowNumber in (10, 11)
--update seat set Information = N'Возможное ограничение видимости до 40%' where AreaDescription like N'%2 ярус,%' and RowNumber in (7,8,9) and SeatNumber in (2,4,6)

--update seat set Information = N'Возможное ограничение видимости до 45%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber = N'1' and SeatNumber between 53 and 56
--update seat set Information = N'Возможное ограничение видимости до 45%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber = N'1' and SeatNumber between 27 and 30
--update seat set Information = N'Возможное ограничение видимости до 45%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber in (N'2', N'3', N'4')

--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Бельэтаж%' and SeatNumber in(N'7', N'8') and RowNumber between 1 and 9
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%1 ярус,%' and SeatNumber in(2,4,6) and RowNumber between 2 and 9
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%1 ярус,%' and SeatNumber in (4,6,8) and RowNumber = 1 and RowName = N'Ложа'
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 2 яруса%' and RowNumber = 3 and SeatNumber between 1 and 4
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 2 and SeatNumber between 1 and 5
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 2 and SeatNumber between 37 and 41
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 3 яруса%' and RowNumber = 3
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber = N'1' and SeatNumber between 1 and 26
--update seat set Information = N'Возможное ограничение видимости до 50%' where AreaDescription like N'%Балкон 4 яруса%' and RowNumber = N'1' and SeatNumber between 57 and 81

--update seat set Information = N'Стоячие места (возможное ограничение видимости до 50%)' where RowNumber in (N'2А', N'3А', N'4А')
