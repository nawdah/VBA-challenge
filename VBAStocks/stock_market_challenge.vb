Sub stock_market()

Dim ws As Worksheet

For Each ws In ActiveWorkbook.Worksheets
    
    ws.Range("I1").Value = "Ticker"
    ws.Range("J1").Value = "Yearly Change"
    ws.Range("K1").Value = "Percent Change"
    ws.Range("L1").Value = "Total Stock Volume"
    
    ws.Range("O2").Value = "Greatest % Increase"
    ws.Range("O3").Value = "Greatest % Decrease"
    ws.Range("O4").Value = "Greatest Total Volume"
    
    ws.Range("P1").Value = "Ticker"
    ws.Range("Q1").Value = "Value"
    

    Dim ticker As String
    Dim last_row As Long
    Dim opening As Double
    Dim closing As Double
    Dim yearly_change As Double
    Dim percent_change As Double
    
    Dim total_stock_volume As Double
    total_stock_volume = 0
    
    Dim greatest_increase As Double
    greatest_increase = WorksheetFunction.Max(ws.Range("K:K"))
    ws.Range("Q2").Value = greatest_increase
    
    Dim greatest_decrease As Double
    greatest_decrease = WorksheetFunction.Min(ws.Range("K:K"))
    ws.Range("Q3").Value = greatest_decrease
    
    Dim greatest_total_volume As Double
    greatest_total_volume = WorksheetFunction.Max(ws.Range("L:L"))
    ws.Range("Q4").Value = greatest_total_volume
    
    Dim row As Long
    
    Dim counter As Long
    counter = 2
    
    
    last_row = Cells(Rows.Count, 1).End(xlUp).row

    For row = 2 To last_row
    
        If ws.Cells(row, 1).Value <> ws.Cells(row - 1, 1).Value Then
        
            opening = ws.Cells(row, 3).Value
            
        End If
        
        If ws.Cells(row, 1).Value <> ws.Cells(row + 1, 1).Value Then
        
            ticker = ws.Cells(row, 1).Value
            closing = ws.Cells(row, 6).Value
            
            yearly_change = closing - opening
            total_stock_volume = total_stock_volume + ws.Cells(row, 7).Value
            
            ws.Range("I" & counter).Value = ticker
            ws.Range("J" & counter).Value = yearly_change
            
            If ws.Range("J" & counter).Value >= 0 Then
                ws.Range("J" & counter).Interior.ColorIndex = 4
            ElseIf ws.Range("J" & counter).Value < 0 Then
                ws.Range("J" & counter).Interior.ColorIndex = 3
            End If
            
            If (opening = 0) Then
                    percent_change = 0
                    ws.Range("K" & counter).Value = percent_change
            Else
                percent_change = yearly_change / opening
                ws.Range("K" & counter).Value = percent_change
            End If
            
            ws.Range("K" & counter).NumberFormat = "0.00%"
            ws.Range("L" & counter).Value = total_stock_volume
            
            counter = counter + 1
            total_stock_volume = 0
       
            Else
            total_stock_volume = total_stock_volume + ws.Cells(row, 7).Value
            yearly_change = closing - opening
            
        End If
    Next row
    
    last_row = ws.Cells(Rows.Count, 9).End(xlUp).row
    
    For row = 2 To last_row
        If ws.Range("K" & row).Value = greatest_increase Then
            ws.Range("Q2").Value = greatest_increase
            ws.Range("Q2").NumberFormat = "0.00%"
            ws.Range("P2").Value = ws.Range("I" & row).Value
        End If
        
        If ws.Range("K" & row).Value = greatest_decrease Then
            ws.Range("Q3").Value = greatest_decrease
            ws.Range("Q3").NumberFormat = "0.00%"
            ws.Range("P3").Value = ws.Range("I" & row).Value
        End If

        If ws.Range("L" & row).Value = greatest_total_volume Then
            ws.Range("Q4").Value = ws.Range("L" & row).Value
            ws.Range("P4").Value = ws.Range("I" & row).Value
        End If

    Next row
        
    ws.Columns("I:Q").AutoFit
    
       
Next ws


End Sub



