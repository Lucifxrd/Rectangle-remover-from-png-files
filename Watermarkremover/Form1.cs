using System.Drawing.Drawing2D;


namespace Watermarkremover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // rectangles to remove from the png images
        public int[,] rectangles = {
                { 372, 483, 524, 589 },
                { 0, 0, 231, 74 },
                { 229, 0, 350, 8},
                { 359, 0, 585, 74},
                {555, 72, 584, 201 },
                { 281, 289, 27, 270},
                {372, 202, 250, 153 },
                {570, 354, 584, 483 },
                {229, 268, 145, 21 },
                {0, 69, 165, 286 },
                { 0, 483, 217, 286 },
                {359, 202, 14, 66 },
                {165, 202, 66, 87 },
                {165, 289, 52, 66 },
                {0, 355, 19, 128 }
         };
        public void button1_Click(object sender, EventArgs e)
        {
            string localdirectory = System.Environment.CurrentDirectory;
            DirectoryInfo Path = new(@$"{localdirectory}\Images");
            DirectoryInfo Path2 = new(@$"{localdirectory}\Cover");
            FileInfo[] cover = Path2.GetFiles("*.png");


            FileInfo[] Files = Path.GetFiles("*.png");


            foreach (FileInfo image in Files)
            {
                Console.WriteLine(image.FullName + "\n\n\n");

                var before = new Bitmap(Image.FromFile($"{image}"));

                // 
                // Bitmap coverBit = new(cover_);
                for (int i = 0; i < rectangles.GetLength(0); i++)
                {
                    int[] rect = { rectangles[i, 0], rectangles[i, 1], rectangles[i, 2], rectangles[i, 3] };
                    Drraw(before, rect[0], rect[1], rect[2], rect[3]);
                }
                Console.WriteLine(cover.GetLength(0) + "  " + Convert.ToString(checkBox2.Checked));
                //Bitmap beforeBit = new Bitmap(before);
                if (checkBox2.Checked)
                {

                    var coverlenght = cover.GetLength(0);

                    if (coverlenght == 0)
                    {
                        Console.WriteLine("cover == 0");
                        textBox1.Text = "no cover or invalid";
                        before.Dispose();
                        break;
                    }
                    else if (coverlenght == 1)
                    {
                        Console.WriteLine("Cover == 1");
                        Bitmap CoverBit;
                        try
                        {
                            CoverBit = new Bitmap(Image.FromFile($"{cover[0]}"));
                            Rectangle rct1 = new Rectangle(0, 0, 585, 559);
                            Rectangle rct2 = new Rectangle(0, 0, 585, 559);

                            CopyRegionIntoImage(before, rct1, CoverBit, rct2);

                            string name2 = image.Name + "-fin.png";

                            Console.WriteLine("trying to save");
                            CoverBit.Save(@$"Done\{name2}");
                            Console.WriteLine("passed save");
                            CoverBit.Dispose();
                        }
                        catch (Exception eee)
                        {
                            Console.WriteLine("theres no valid image cover on folder \n" + eee + "\n");
                            textBox1.Text = "[error]Cover not valid check console";
                        }
                        finally
                        {
                            before.Dispose();
                            textBox1.Text = "Done";
                        }
                    }
                    else
                        textBox1.Text = "[error]more than 1 cover";
                    continue;

                }

                string name = image.Name;
                before.Save(@$"Done\{name}");
                textBox1.Text = "Done";
                before.Dispose();
                
            }


            

        }
    
    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    // this receive the image that will be edited, the rectangle posittion(left upper corner position of the rectangle(x,y)) 
    // then the width and height of the rectangle(w,h)
    public static void Drraw(Image beffore, int x, int y, int w, int h)
    {
        var gr = Graphics.FromImage(beffore);
        GraphicsPath gp = new GraphicsPath();

        // clr
        gp.AddRectangle(new Rectangle(x, y, w, h));
        gr.SetClip(gp);
        gr.Clear(Color.Transparent);
    }
    public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, Bitmap destBitmap, Rectangle destRegion)
    {
        using (Graphics grD = Graphics.FromImage(destBitmap))
        {
            grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {

    }

    }


}

   

