Imports System.Linq

Namespace Fields
    Friend Class FieldCollection
        Inherits System.Collections.Generic.Dictionary(Of String, Fields.BaseField)

        Overloads ReadOnly Property Item(i As Integer) As Fields.BaseField
            Get
                Return Values.ElementAt(i)
            End Get
        End Property
    End Class

End Namespace
