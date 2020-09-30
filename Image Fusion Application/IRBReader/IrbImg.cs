using System;
using System.Windows.Forms;

namespace Image_Fusion_Application
{
    class IrbImg
    {
        //private static Logging logging = new Logging("IrbImg");


        public double ShotRangeMin { get; protected set; }
        public double ShotRangeMax { get; protected set; }
        public double CalibRangeMin { get; protected set; }
        public double CalibRangeMax { get; protected set; }


        IrbFileReader reader;

        int BytePerPixel;
        int Compressed;
        int UpperLeftX;
        int UpperLeftY;
        int FirstValidX;
        int LastValidX;
        int FirstValidY;
        int LastValidY;
        float Position;

        float Emissivity;

        public float EnvironmentalTemp;
        float Distanz;

        float AbsoConst;
        float PathTemperature;
        long Version;
        int[] cbData = new int[1567];

        float Level;
        float Span;
        double ImgTime;
        int ImgMilliTime;
        int ImgAccu;
        string ImageComment;
        float ZoomVertical;
        float ZoomHorizontal;
        float CenterWavelength;

        float CalibRange_min;
        float CalibRange_max;

        float ShotRange_start_ERROR;
        float ShotRange_size;

        double TimeStamp_Raw;
        DateTime TimeStamp;
        DateTime TimeStampOffsetTime;

        int TimeStampOffsetMilliseconds;
        int TimeStampMilliseconds;

        string Device;
        string DeviceSerial;
        string Optics;
        string OpticsResolution;
        string OpticsText;

        public float[] Data;
        public float[,,] VisibleData;
        public float[,] tempMatrix;

        private int Width;
        private int Height;
        public float[] Palette;
        public float[] VisiblePalette;


        public IrbImg(IrbFileReader FileReader, string imgType, float[] palette = null, int imageIndex = 0)
        {
            try
            {
                reader = FileReader;
                if (palette != null)
                    Palette = palette;
                if (imgType == "visible")
                    ReadVisibleImage(imageIndex);
                else
                    ReadImage(imageIndex);
            }
            catch (Exception e) {
                MessageBox.Show("IrbImage feldolgozás hiba!\n" + e.Message);
            }
        }


        /// <summary>
        /// Width of the image
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return Width;
        }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int GetHeight()
        {
            return Height;
        }

        /// <summary>
        /// Return the data of the image as float array
        /// </summary>
        /// <returns></returns>
        public float[] GetData()
        {
            if (Data == null) return null;//logging.addError("getData() Accessing non initialised data!");
            return Data;
        }

        public float[,,] GetVisibleData() 
        {
            if (VisibleData == null) return null;//logging.addError("getData() Accessing non initialised data!");
            return VisibleData;
        }


        /// <summary>
        /// Read a image from the file
        /// </summary>
        public bool ReadImage(int imageIndex)
        {
            System.DateTime FrameTime = System.DateTime.Now;


            var reader = new BufferReader(this.reader.GetImageData(imageIndex));

            if (reader.Eof)
            {
                return false;
            }

            Width = 0;
            Height = 0;
            var i = 0;

            //- Image header
            BytePerPixel = reader.ReadWordBE();
            Compressed = reader.ReadWordBE();
            Width = reader.ReadWordBE();
            Height = reader.ReadWordBE();


            UpperLeftX = reader.ReadWordBE(); 
            UpperLeftY = reader.ReadWordBE(); 

            FirstValidX = reader.ReadWordBE();


            LastValidX = reader.ReadWordBE(); 


            FirstValidY = reader.ReadWordBE();


            LastValidY = reader.ReadWordBE(); 
            Position = reader.ReadSingleBE(); 

            Emissivity = reader.ReadSingleBE();

            Distanz = reader.ReadSingleBE();

            EnvironmentalTemp = reader.ReadSingleBE();


            AbsoConst = reader.ReadSingleBE(); 
            PathTemperature = reader.ReadSingleBE(); 
            Version = reader.ReadLongBE(); 

            //for (int pos = 0; pos < 1567; pos++)
            //    cbData[pos] = reader.ReadByte();

            Level = reader.ReadSingleBE(); 
            Span = reader.ReadSingleBE();
            ImgTime = reader.ReadDoubleBE(8);
            ImgMilliTime = reader.ReadIntBE();
            //TimeStamp = Double2DateTime(ImgTime, (int)ImgMilliTime);
            ImgAccu = reader.ReadWordBE();
            ImageComment = reader.ReadStr(79,0);
            ZoomHorizontal = reader.ReadSingleBE();
            ZoomVertical = reader.ReadSingleBE();


            i = reader.ReadWordBE(); 
            i = reader.ReadWordBE(); 
            i = reader.ReadWordBE(); 
            i = reader.ReadWordBE(); 


            if ((Width > 10000) || (Height > 10000))
            {
                Width = 1;
                Height = 1;
                return false;
            }

            this.ReadFlags(reader, 1084);

            Data = ReadImageData(reader, 0x6C0, Width, Height, 60, Compressed);



            if (reader.Eof) return false; 

            return true;
        }

        public bool ReadVisibleImage(int imageIndex)
        {
            System.DateTime FrameTime = System.DateTime.Now;


            var reader = new BufferReader(this.reader.GetImageData(imageIndex));

            if (reader.Eof)
            {
                return false;
            }

            Width = 0;
            Height = 0;


            //- Image header
            BytePerPixel = reader.ReadIntBE();
            Compressed = reader.ReadIntBE();
            Width = reader.ReadIntBE();
            Height = reader.ReadIntBE();

            VisibleData = ReadVisibleImageData(reader, 8468, Width, Height, 276, Compressed);

            if (reader.Eof) return false; 

            return true;
        }


        /// <summary>
        /// Read image flags from the file
        /// </summary>
        public void ReadFlags(BufferReader reader, int offset)
        {
            CalibRange_min = reader.ReadSingleBE(offset + 92);
            CalibRange_max = reader.ReadSingleBE(offset + 96);


            Device = reader.ReadNullTerminatedString(offset + 142, 12);
            DeviceSerial = reader.ReadNullTerminatedString(offset + 186, 16);
            Optics = reader.ReadNullTerminatedString(offset + 202, 32);
            OpticsResolution = reader.ReadNullTerminatedString(offset + 234, 32);
            OpticsText = reader.ReadNullTerminatedString(offset + 554, 48);

            ShotRange_start_ERROR = reader.ReadSingleBE(offset + 532);
            ShotRange_size = reader.ReadSingleBE(offset + 536);


            TimeStamp_Raw = reader.ReadDoubleBE(8, offset + 540);
            TimeStampMilliseconds = reader.ReadIntBE(offset + 548);
            ImgAccu = reader.ReadWordBE(offset + 580);
            ZoomHorizontal = reader.ReadWordBE(offset + 596);
            ZoomVertical = reader.ReadWordBE(offset + 604);
            TimeStamp = Double2DateTime(TimeStamp_Raw, TimeStampMilliseconds);
        }




        /// <summary>
        /// Read the compressing "pallet" from file 
        /// </summary>
        private float[] ReadPallet(BufferReader reader, int offset)
        {
            float[] palette = new float[256];

            int pos = offset;

            for (int i = 0; i < 256; i++)
            {
                 palette[i] = reader.ReadSingleBE(pos);
                 pos += 4;
            }

            return palette;
        }

        private float[] ReadVisiblePallet(BufferReader reader, int offset)
        {
            float[] palette = new float[256];

            int pos = offset;

            for (int i = 0; i < 256; i++)
            {
                palette[i] = reader.ReadSingleBE(pos);
                pos += 4;
            }

            return palette;
        }



        /// <summary>
        /// Read the image data from file
        /// </summary>
        /// <returns></returns>
        private float[] ReadImageData(BufferReader reader, int bindata_offset, int width, int height, int palette_offset, int useCompression)
        {
            int data_size = width * height; //- count of pixles
            bool useComp = (useCompression == 1);

            int pixelCount = data_size;
            float[] matrixData = new float[pixelCount];

            int matrixDataPos = 0;

            int v1_pos = bindata_offset;
            int v2_pos = v1_pos + width * height; //- used if data are compressed

            //byte data_v1 = &bindata[v1_pos];
            //unsigned char* data_v2 = &bindata[v2_pos];

            int v1 = 0;
            int v2 = 0;


            Palette = ReadPallet(reader, palette_offset);


            int v2_count = 0;
            float v = 0;
            float f;

            if (!useComp)
            {
                for (int i = pixelCount; i > 0; i--)
                {
                    //- read values
                    v1 = reader.ReadByte(v1_pos);
                    v1_pos++;
                    v2 = reader.ReadByte(v1_pos);
                    v1_pos++;

                    f = (float)v1 * (1.0f / 256.0f);

                    //- lineare interpolation
                    v = Palette[v2 + 1] * f + Palette[v2] * (1.0f - f);

                    if (v < 0) v = 0; //- oder 255

                    matrixData[matrixDataPos] = v;
                    matrixDataPos++;
                }
            }
            else
            {
                for (int i = pixelCount; i > 0; i--)
                {
                    //- werte lesen
                    if (v2_count-- < 1) //- ok... neuen wert für V2 lesen
                    {
                        v2_count = reader.ReadByte(v2_pos) - 1;
                        v2_pos++;

                        v2 = reader.ReadByte(v2_pos);
                        v2_pos++;
                    }

                    v1 = reader.ReadByte(v1_pos);
                    v1_pos++;

                    f = (float)v1 * (1.0f / 256.0f);

                    //- lineare interpolation
                    v = Palette[v2 + 1] * f + Palette[v2] * (1.0f - f);

                    if (v < 0) v = 0; //- oder 255

                    matrixData[matrixDataPos] = v;
                    matrixDataPos++;
                }
            }
            tempMatrix = new float[Width, Height];
            for (matrixDataPos = 0; matrixDataPos < pixelCount; matrixDataPos++)
            {
                var i = matrixDataPos % Width;
                var j = matrixDataPos / Width;
                tempMatrix[i, j] = matrixData[matrixDataPos];
            }
            return matrixData;

        }

        private float[,,] ReadVisibleImageData(BufferReader reader, int bindata_offset, int width, int height, int palette_offset, int useCompression)
        {
            int data_size = width * height; //- count of pixles
            bool useComp = (useCompression == 1);

            int pixelCount = data_size;
            float[] matrixData = new float[pixelCount];

            int matrixDataPos = 0;

            int v1_pos = bindata_offset;
            int v2_pos = v1_pos + width * height; //- used if data are compressed

            //byte data_v1 = &bindata[v1_pos];
            //unsigned char* data_v2 = &bindata[v2_pos];

            int v1 = 0;
            int v2 = 0;
            int v3 = 0;
            int v4 = 0;
            var argb = 0;
            float v = 0;
            float f,f1,f2,f3;

            VisiblePalette = ReadVisiblePallet(reader, palette_offset);

            if (!useComp)
            {
                for (int i = pixelCount; i > 0; i--)
                {
                    //- read value
                    v1 = reader.ReadByte(v1_pos);
                    v1_pos++;
                    v2 = reader.ReadByte(v1_pos);
                    v1_pos++;

                    f1 = (float)v1 * (1.0f / 256.0f);
                    f2 = (float)v2 * (1.0f / 256.0f);
                    f3 = (float)v3 * (1.0f / 256.0f);

                    //- lineare interpolation
                    v = Palette[v1 + 1] * f1 + Palette[v2] * (1.0f - f1); //v1 v2

                    if (v < 0) v = 0; //- oder 255

                    matrixData[matrixDataPos] = v;
                    matrixDataPos++;
                }
            }
            else
            {
                for (int i = pixelCount; i > 0; i--)
                {
                    //- read value
                    v1 = reader.ReadByte(v1_pos);
                    v1_pos++;
                    v2 = reader.ReadByte(v1_pos);
                    v1_pos++;

                    f1 = (float)v1 * (1.0f / 256.0f);
                    f2 = (float)v2 * (1.0f / 256.0f);
                    f3 = (float)v3 * (1.0f / 256.0f);

                    //- lineare interpolation
                    v = Palette[v2 + 1] * f1 + Palette[v1] * (1.0f - f1); //v1 v2

                    if (v < 0) v = 0; //- oder 255

                    matrixData[matrixDataPos] = v;
                    matrixDataPos++;
                }
            }

            float[,] imageMatrix = new float[Width,Height];
            
            for (matrixDataPos = 0; matrixDataPos < pixelCount; matrixDataPos++) {
                    var i = matrixDataPos % Width;
                    var j = matrixDataPos / Width;
                    imageMatrix[i, j] = matrixData[matrixDataPos];
            }

            var minValue = float.MaxValue;
            var maxValue = float.MinValue;

            for(int i = 0; i < Width; i++)
                for(int j = 0; j < Height; j++)
                {
                    maxValue = Math.Max(maxValue, imageMatrix[i, j]);
                    minValue = Math.Min(minValue, imageMatrix[i, j]);
                }

            var imageScale = 255.0f / (maxValue - minValue);

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    imageMatrix[i,j] = (int)((imageMatrix[i, j] - minValue) * imageScale);

            float[,,] rgbImageMatrix = new float[Width, Height, 3];

            for (int i = 1; i < Width - 1; i++)
                for (int j = 1; j < Height - 1; j++) {
                    //blue
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        rgbImageMatrix[i, j, 0] = (imageMatrix[i - 1, j - 1] + imageMatrix[i - 1, j + 1] + imageMatrix[i + 1, j - 1] + imageMatrix[i + 1, j + 1]) / 4;
                        rgbImageMatrix[i, j, 1] = (imageMatrix[i - 1, j] + imageMatrix[i, j - 1] + imageMatrix[i, j + 1] + imageMatrix[i + 1, j]) / 4;
                        rgbImageMatrix[i, j, 2] = imageMatrix[i, j];
                    }
                    //green
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)) {
                        rgbImageMatrix[i, j, 2] = (imageMatrix[i - 1, j] + imageMatrix[i + 1, j]) / 2;
                        rgbImageMatrix[i, j, 1] = imageMatrix[i, j];
                        rgbImageMatrix[i, j, 0] = (imageMatrix[i, j - 1] + imageMatrix[i, j + 1]) / 2;
                    }
                    //red
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        rgbImageMatrix[i, j, 0] = imageMatrix[i, j];
                        rgbImageMatrix[i, j, 1] = (imageMatrix[i - 1, j] + imageMatrix[i, j - 1] + imageMatrix[i, j + 1] + imageMatrix[i + 1, j]) / 4;
                        rgbImageMatrix[i, j, 2] = (imageMatrix[i - 1, j - 1] + imageMatrix[i - 1, j + 1] + imageMatrix[i + 1, j - 1] + imageMatrix[i + 1, j + 1]) / 4;

                        //float[] mat = { rgbImageMatrix[i, j, 0], rgbImageMatrix[i, j, 1], rgbImageMatrix[i, j, 2] };

                        //var res = applySaturation(mat, 1.5f);
                        //for (int k = 0; k < 3; k++)
                        //    rgbImageMatrix[i, j, k] = res[k];
                    } 
                }

            return rgbImageMatrix;
        }

        private float[] applySaturation(float[] rgb, float k)
        {
            float[,] satMatrix = 
                { {0.299f + 0.701f * k, 0.587f * (1 - k), 0.114f * (1 - k) },
                { 0.299f * (1 - k), 0.587f + 0.413f * k, 0.114f * (1 - k) },
                { 0.299f * (1 - k), 0.587f * (1 - k), 0.114f + 0.886f * k } };

            float[] res = new float[3];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++) {
                    res[i] += satMatrix[i,j] * rgb[j]; 
                }
            return res;
        }

        /// <summary>
        /// convert a double value to a date time
        /// </summary>
        private System.DateTime Double2DateTime(double date, int Milliseconds = 0)
        {
            System.DateTime d = DateTime.FromOADate(date);

            //- calc the time from the Date + Milliseconds
            if ((TimeStampOffsetTime != DateTime.MinValue) && (Milliseconds > 0) && (Milliseconds > TimeStampOffsetMilliseconds) && (d >= TimeStampOffsetTime))
            {
                return TimeStampOffsetTime.AddMilliseconds(Milliseconds - TimeStampOffsetMilliseconds);
            }
            else
            {
                //- never set so save start-date/time
                TimeStampOffsetMilliseconds = Milliseconds;
                TimeStampOffsetTime = d;
                return d;
            }

        }


    }
}
