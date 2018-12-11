Namespace Utils.FileSystem.Impl.Date
    Public Class IM_JsDate
        Overloads Shared Function GetTimeInMills() As Long
            Return Decimal.ToInt64(Decimal.Divide(Date.Now.Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(afterSet As Short) As Long
            Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddSeconds(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(afterSet As Short, afterType As Short) As Long
            Select Case AfterType
                Case 0
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddSeconds(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
                Case 1
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddMinutes(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
                Case 2
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddHours(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
                Case 3
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddDays(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
                Case 4
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddMonths(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
                Case Else
                    Return Decimal.ToInt64(Decimal.Divide(Date.Now.AddYears(AfterSet).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
            End Select
        End Function

        Overloads Shared Function GetTimeInMills(dTime As Date) As Long
            Return Decimal.ToInt64(Decimal.Divide(dTime.Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, 1, 1).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer, month As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, month, 1).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer, month As Integer, day As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, month, day).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer, month As Integer, day As Integer, hour As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, month, day, hour, 0, 0).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer, month As Integer, day As Integer, hour As Integer, minute As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, month, day, hour, minute, 0).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetTimeInMills(year As Integer, month As Integer, day As Integer, hour As Integer, minute As Integer, second As Integer) As Long
            Return Decimal.ToInt64(Decimal.Divide(New Date(year, month, day, hour, minute, second).Ticks - New Date(1970, 1, 1, 8, 0, 0).Ticks, 10000))
        End Function

        Overloads Shared Function GetDateFromTimeInMills(timeInMillis As Long) As Date
            Return Date.FromBinary(Long.Parse(Decimal.Multiply(TimeInMillis, 10000) + New Date(1970, 1, 1, 8, 0, 0).Ticks))
        End Function

        Overloads Shared Function GetDateFromTimeInMills(timeInMillis As String) As Date
            Try
                Return Date.FromBinary(Long.Parse(Decimal.Multiply(TimeInMillis, 10000) + New Date(1970, 1, 1, 8, 0, 0).Ticks))
            Catch ex As Exception
                Return New Date(1970, 1, 1, 8, 0, 0)
            End Try
        End Function

        Overloads Shared Function GetDateStringFromTimeInMills(timeInMillis As Long, stringFormat As String) As String
            Try
                Return Date.FromBinary(Long.Parse(Decimal.Multiply(TimeInMillis, 10000) + New Date(1970, 1, 1, 8, 0, 0).Ticks)).ToString(StringFormat)
            Catch ex As Exception
                Return New Date(1970, 1, 1, 8, 0, 0).ToString("yyyy-MM-dd HH:mm:ss")
            End Try
        End Function

        Shared Function GetNow() As DateTime
            Return DateTime.Now
        End Function

        Shared Function GetNowDateTimeExStr() As String
            Return $"{DateTime.Now:yyyyMMdd_HHmmss_ffffff}"
        End Function

        Shared Function GetNowDateTimeStr() As String
            Return $"{DateTime.Now:yyyyMMdd_HHmmss}"
        End Function

        Shared Function GetNowDateStr() As String
            Return $"{DateTime.Now:yyyyMMdd}"
        End Function

        Shared Function GetNowTimeStr() As String
            Return $"{DateTime.Now:HHmmss}"
        End Function

        Shared Function GetNowStr(stringFormat As String) As String
            Dim nowStr As String = String.Empty
            Try
                NowStr = DateTime.Now.ToString(StringFormat)
            Catch ex As Exception
                nowStr = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            End Try
            Return NowStr
        End Function

        Shared Function GetDateStr(dt As Date, stringFormat As String) As String
            Dim dtStr As String = String.Empty
            Try
                dtStr = dt.ToString(StringFormat)
            Catch ex As Exception
                dtStr = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            End Try
            Return dtStr
        End Function

        Shared Function UnixToDate(Byval timeSpan As Long) As Date
            Dim dtStart As Date = New Date(1970, 1, 1)
            '15166928611522404
            Dim tmpSpan As String = timeSpan.ToString()
            If tmpSpan.Length = 11 Then
                tmpSpan = tmpSpan + "000000"
            ElseIf tmpSpan.Length = 10 Then
                tmpSpan = tmpSpan + "0000000"
            End If
            Dim lTime As Long = Long.Parse(tmpSpan)
            Dim toNow As New TimeSpan(lTime)
            Dim dtResult As Date = dtStart.Add(toNow)
            Return dtResult
        End Function

        Shared Function DateToUnix(Byval dtNow As Date) As String
            Dim dtStart As Date = New Date(1970, 1, 1)
            Dim toNow As TimeSpan = dtNow.Subtract(dtStart)
            Dim timeStamp As String = toNow.Ticks.ToString()
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7)
            Return timeStamp
        End Function
    End Class
End Namespace
