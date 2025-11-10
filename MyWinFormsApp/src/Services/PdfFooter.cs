using iTextSharp.text;
using iTextSharp.text.pdf;

public class PdfFooter: PdfPageEventHelper
{
    PdfTemplate total;
    BaseFont helv;

    public override void OnOpenDocument( PdfWriter writer, Document document )
    {
        total = writer.DirectContent.CreateTemplate( 50, 50 );
        helv = BaseFont.CreateFont( BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED );
    }

    public override void OnEndPage( PdfWriter writer, Document document )
    {
        int pageN = writer.PageNumber;
        string text = "Página " + pageN + " de ";
        float len = helv.GetWidthPoint( text, 9 );

        PdfContentByte cb = writer.DirectContent;
        float x = document.PageSize.GetRight( 80 );
        float y = document.PageSize.GetLeft( 20 );

        cb.BeginText();
        cb.SetFontAndSize( helv, 9 );
        cb.SetTextMatrix( x, y );
        cb.ShowText( text );
        cb.EndText();

        cb.AddTemplate( total, x + len, y );
    }

    public override void OnCloseDocument( PdfWriter writer, Document document )
    {
        total.BeginText();
        total.SetFontAndSize( helv, 9 );
        total.SetTextMatrix( 0, 0 );
        total.ShowText( ( writer.PageNumber ).ToString() );
        total.EndText();
    }
}
