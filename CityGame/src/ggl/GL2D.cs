using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
//using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GGL
{
    public struct ImageDrawData
    {
        public Texture Texture;
        public Rectangle Src;
        public Rectangle Dst;
        public Color Color;
        public ImageDrawData(Texture texture, Rectangle src, Rectangle dst, Color color)
        {
            this.Texture = texture;
            this.Src = src;
            this.Dst = dst;
            this.Color = color;
        }
        public void Update(Texture texture, Rectangle src, Rectangle dst, Color color)
        {
            this.Texture = texture;
            this.Src = src;
            this.Dst = dst;
            this.Color = color;
        }
    }
    public struct SquareDrawData
    {
        public Texture Texture;
        public Point[] Src;
        public Point[] Dst;
        public Color Color;
        public SquareDrawData(Texture texture, Point[] src, Point[] dst, Color color)
        {
            this.Texture = texture;
            this.Src = src;
            this.Dst = dst;
            this.Color = color;
        }
        public void Update(Texture texture, Point[] src, Point[] dst, Color color)
        {
            this.Texture = texture;
            this.Src = src;
            this.Dst = dst;
            this.Color = color;
        }
    }
    public class Texture
    {
        public int ID;
        public int Width;
        public int Height;
        public Texture(string file)
        {
            try
            {
                Bitmap bitmap = new Bitmap(file);
                Width = bitmap.Width;
                Height = bitmap.Height;
                GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

                GL.GenTextures(1, out ID);
                GL.BindTexture(TextureTarget.Texture2D, ID);

                System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                
                Console.WriteLine("load: " + file + " |ID: "+ ID);
            }
            catch
            {
                Width = 0;
                Height = 0;
                ID = 0;
                Console.WriteLine("ERROR => load: " + file);
            }
        }

    }


    public static class GL2D
    {
        private static int width = 640, height = 480;

        private static int lastVertexOffset, lastIndexOffset, vertexOffset, indexOffset, textureOffset, renderTextureOffset;

        private static int positionAttrib, texturePosAttrib, colorAttrib, textureIndexAttrib;

        private static int positionBuffer, texturePosBuffer, colorBuffer, textureIndexBuffer, indexBuffer;

        private static Vector2[] positionData;
        private static Vector2[] texturePosData;
        private static Vector4[] colorData;
        private static Vector4h[] textureIndexData;

        private static int colorUniform, samplerUniform, timeUniform, resolutionUniform;

        public static Color GlobalColor = Color.White;
        private static int[] indexData;

        private static int[] textureList;
        private static int[] textureContinuous;

        private static int lastTextureID;
        private static int shaderProgram;

        public static void UpdateSize(Size input)
        {
            width = input.Width;
            height = input.Height;
        }

        public static void drawTriangle(Texture texture, Point[] src, Point[] dst, Color[] color)
        {
            PointF[] dstF = new PointF[3] { new PointF(src[0].X / (width / 2f), -src[0].Y / (height / 2f)), new PointF(src[1].X / (width / 2f), -src[1].Y / (height / 2f)), new PointF(src[2].X / (width / 2f), -src[2].Y / (height / 2f)) };
            PointF[] srcF = new PointF[3] { new PointF(dst[0].X / (float)texture.Width, dst[0].Y / (float)texture.Height), new PointF(dst[1].X / (float)texture.Width, dst[1].Y / (float)texture.Height), new PointF(dst[2].X / (float)texture.Width, dst[2].Y / (float)texture.Height) };

            positionData[vertexOffset + 0] = new Vector2(dstF[0].X, dstF[0].Y);
            positionData[vertexOffset + 1] = new Vector2(dstF[1].X, dstF[1].Y);
            positionData[vertexOffset + 2] = new Vector2(dstF[2].X, dstF[2].Y);

            texturePosData[vertexOffset + 0] = new Vector2(srcF[0].X, srcF[0].Y);
            texturePosData[vertexOffset + 1] = new Vector2(srcF[1].X, srcF[1].Y);
            texturePosData[vertexOffset + 2] = new Vector2(srcF[2].X, srcF[2].Y);

            colorData[vertexOffset + 0] = new Vector4(color[0].R, color[0].G, color[0].B, color[0].A);
            colorData[vertexOffset + 1] = new Vector4(color[1].R, color[1].G, color[1].B, color[1].A);
            colorData[vertexOffset + 2] = new Vector4(color[2].R, color[2].G, color[2].B, color[2].A);

            indexData[indexOffset + 0] = vertexOffset + 0;
            indexData[indexOffset + 1] = vertexOffset + 1;
            indexData[indexOffset + 2] = vertexOffset + 2;

            //GL.ActiveTexture(TextureUnit.
            vertexOffset += 3;
            indexOffset += 3;

            if (texture.ID != lastTextureID)
            {
                textureOffset++;
                lastTextureID = texture.ID;
                textureList[textureOffset] = texture.ID;
                textureContinuous[textureOffset] = 0;
            }
            textureContinuous[textureOffset] += 1;
        }

        public static void drawImage(ImageDrawData data)
        {
            drawImage(data.Texture, data.Src, data.Dst, data.Color);
        }
        public static void drawImage(ImageDrawData[] data, int length)
        {
            for (int i = 0; i < length; i++)
            {
                drawImage(data[i].Texture, data[i].Src, data[i].Dst, data[i].Color);
            }
        }
        public static void drawImage(Texture texture, Rectangle src, Rectangle dst, Color color)
        {
            RectangleF dstF = new RectangleF(dst.X / (width / 2f) - 1, -(dst.Y / (height / 2f) - 1), (dst.X + dst.Width) / (width / 2f) - 1, -((dst.Y + dst.Height) / (height / 2f) - 1));
            RectangleF srcF = new RectangleF(src.X / (float)texture.Width, src.Y / (float)texture.Height, (src.X + src.Width) / (float)texture.Width, (src.Y + src.Height) / (float)texture.Height);

            positionData[vertexOffset + 0] = new Vector2(dstF.X, dstF.Y);
            positionData[vertexOffset + 1] = new Vector2(dstF.Width, dstF.Y);
            positionData[vertexOffset + 2] = new Vector2(dstF.Width, dstF.Height);
            positionData[vertexOffset + 3] = new Vector2(dstF.X, dstF.Height);

            texturePosData[vertexOffset + 0] = new Vector2(srcF.X, srcF.Y);
            texturePosData[vertexOffset + 1] = new Vector2(srcF.Width, srcF.Y);
            texturePosData[vertexOffset + 2] = new Vector2(srcF.Width, srcF.Height);
            texturePosData[vertexOffset + 3] = new Vector2(srcF.X, srcF.Height);

            colorData[vertexOffset + 0] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 1] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 2] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 3] = new Vector4(color.R, color.G, color.B, color.A);

            indexData[indexOffset + 0] = vertexOffset + 0;
            indexData[indexOffset + 1] = vertexOffset + 1;
            indexData[indexOffset + 2] = vertexOffset + 2;
            indexData[indexOffset + 3] = vertexOffset + 2;
            indexData[indexOffset + 4] = vertexOffset + 3;
            indexData[indexOffset + 5] = vertexOffset + 0;

            ////GL.ActiveTexture(TextureUnit.
            vertexOffset += 4;
            indexOffset += 6;

            int textureID = texture.ID;
            if (textureID != lastTextureID)
            {
                textureOffset += 1;
                lastTextureID = textureID;
                textureList[textureOffset] = textureID;
                textureContinuous[textureOffset] = 0;
            }
            textureContinuous[textureOffset] += 2;
        }

        public static void drawGround(Texture texture, byte[] texureOffset, Point[] src, Point[] dst, Color color)
        {

            textureIndexData[vertexOffset + 0] = new Vector4h(texureOffset[0], texureOffset[1], texureOffset[2], texureOffset[3]);
            drawSquare(texture, src, dst, color);


        }

        public unsafe static void drawSquare(SquareDrawData data)
        {
            drawSquare(data.Texture, data.Src, data.Dst, data.Color);
        }
        public unsafe static void drawSquare(SquareDrawData[] data, int length)
        {
            for (int i = 0; i < length; i++)
            {
                drawSquare(data[i].Texture, data[i].Src, data[i].Dst, data[i].Color);
            }
        }
        public unsafe static void drawSquare(Texture texture, Point[] src, Point[] dst, Color color)
        {


            Vector2[] dstPos = new Vector2[4];// = dst;// new int[] { 0, 0, 0 };//useMatrix(dst);
            for (int i = 0; i < 4; i++)
            {
                dstPos[i] = new Vector2((dst[i].X / (width / 2f) - 1), -(dst[i].Y / (height / 2f) - 1));
            }

            int imageWidth = texture.Width, imageHeight = texture.Height;

            positionData[vertexOffset + 0] = dstPos[0];
            positionData[vertexOffset + 1] = dstPos[1];
            positionData[vertexOffset + 2] = dstPos[2];
            positionData[vertexOffset + 3] = dstPos[3];
            positionData[vertexOffset + 4] = new Vector2((dstPos[0].X + dstPos[1].X + dstPos[2].X + dstPos[3].X) * 0.25f, (dstPos[0].Y + dstPos[1].Y + dstPos[2].Y + dstPos[3].Y) * 0.25f);

            texturePosData[vertexOffset + 0] = new Vector2(src[0].X / (float)imageWidth, src[0].Y / (float)imageHeight);
            texturePosData[vertexOffset + 1] = new Vector2(src[1].X / (float)imageWidth, src[1].Y / (float)imageHeight);
            texturePosData[vertexOffset + 2] = new Vector2(src[2].X / (float)imageWidth, src[2].Y / (float)imageHeight);
            texturePosData[vertexOffset + 3] = new Vector2(src[3].X / (float)imageWidth, src[3].Y / (float)imageHeight);
            texturePosData[vertexOffset + 4] = new Vector2(
                (texturePosData[vertexOffset + 0].X + texturePosData[vertexOffset + 1].X + texturePosData[vertexOffset + 2].X + texturePosData[vertexOffset + 3].X) * 0.25f,
                (texturePosData[vertexOffset + 0].Y + texturePosData[vertexOffset + 1].Y + texturePosData[vertexOffset + 2].Y + texturePosData[vertexOffset + 3].Y) * 0.25f
            );

            colorData[vertexOffset + 0] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 1] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 2] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 3] = new Vector4(color.R, color.G, color.B, color.A);
            colorData[vertexOffset + 4] = new Vector4(color.R, color.G, color.B, color.A);

            indexData[indexOffset + 0] = vertexOffset + 0;
            indexData[indexOffset + 1] = vertexOffset + 1;
            indexData[indexOffset + 2] = vertexOffset + 4;

            indexData[indexOffset + 3] = vertexOffset + 1;
            indexData[indexOffset + 4] = vertexOffset + 2;
            indexData[indexOffset + 5] = vertexOffset + 4;

            indexData[indexOffset + 6] = vertexOffset + 2;
            indexData[indexOffset + 7] = vertexOffset + 3;
            indexData[indexOffset + 8] = vertexOffset + 4;

            indexData[indexOffset + 9] = vertexOffset + 3;
            indexData[indexOffset + 10] = vertexOffset + 0;
            indexData[indexOffset + 11] = vertexOffset + 4;

            indexOffset += 12;
            vertexOffset += 5;

            if (texture.ID != lastTextureID)
            {
                textureOffset++;
                lastTextureID = texture.ID;
                textureList[textureOffset] = texture.ID;
                textureContinuous[textureOffset] = 0;
            }
            textureContinuous[textureOffset] += 4;


        }

        public static int CreateShaders(string pathVS, string pathFS)
        {
            try
            {
                int vertexShader = GL.CreateShader(ShaderType.VertexShader);
                int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            

                int status_code;
                string info;

                GL.ShaderSource(vertexShader, File.ReadAllText(pathVS));
                GL.CompileShader(vertexShader);
                GL.GetShaderInfoLog(vertexShader, out info);
                GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out status_code);
                if (status_code != 1)
                {
                    Console.WriteLine("vertexShader:\n" + info);
                    return -1;
                }


                GL.ShaderSource(fragmentShader, File.ReadAllText(pathFS));
                GL.CompileShader(fragmentShader);
                GL.GetShaderInfoLog(fragmentShader, out info);
                GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status_code);
                if (status_code != 1)
                {
                    Console.WriteLine("fragmentShader:\n" + info);
                    return -1;
                }

                // Create program
                shaderProgram = GL.CreateProgram();

                GL.AttachShader(shaderProgram, vertexShader);
                GL.AttachShader(shaderProgram, fragmentShader);

                GL.LinkProgram(shaderProgram);
                //GL.UseProgram(shaderProgram);
                //GL.Uniform1(shaderProgram, pos);
                return shaderProgram;
            }
            catch (AccessViolationException e)
            {
                return -2;
            }

        }
        public static void UseShader(int shaderProgram) {

            GL.UseProgram(shaderProgram);

            // attributes
            positionAttrib = GL.GetAttribLocation(shaderProgram, "aPosition");
            GL.EnableVertexAttribArray(positionAttrib);

            texturePosAttrib = GL.GetAttribLocation(shaderProgram, "aTexturePos");
            GL.EnableVertexAttribArray(texturePosAttrib);

            textureIndexAttrib = GL.GetAttribLocation(shaderProgram, "aTextureIndex");
            GL.EnableVertexAttribArray(texturePosAttrib);

            colorAttrib = GL.GetAttribLocation(shaderProgram, "aColor");
            GL.EnableVertexAttribArray(colorAttrib);

            //uniforms
            samplerUniform = GL.GetUniformLocation(shaderProgram, "uSampler");

            timeUniform = GL.GetUniformLocation(shaderProgram, "uTime");

            resolutionUniform = GL.GetUniformLocation(shaderProgram, "uResolution");

            colorUniform = GL.GetUniformLocation(shaderProgram, "uColor");

        }

        public static void ClearBuffer()
        {
            GL.Viewport(0, 0, width, height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        public static void CreateBuffer(int bufferSize)
        {
            textureList = new int[bufferSize];
            textureContinuous = new int[bufferSize];

            positionData = new Vector2[bufferSize * 3];
            texturePosData = new Vector2[bufferSize * 3];
            textureIndexData = new Vector4h[bufferSize * 3];
            for (int i = 0; i < textureIndexData.Length; i++) textureIndexData[i] = new Vector4h(0,0,1,1);
            colorData = new Vector4[bufferSize * 3];
            indexData = new int[bufferSize * 3];

            GL.GenBuffers(1, out positionBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBuffer);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, new IntPtr(positionData.Length * 8), positionData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(positionAttrib, 2, VertexAttribPointerType.Float, true, 0, 0);

            GL.GenBuffers(1, out texturePosBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, texturePosBuffer);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, new IntPtr(texturePosData.Length * 8), texturePosData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(texturePosAttrib, 2, VertexAttribPointerType.Float, true, 0, 0);

            GL.GenBuffers(1, out textureIndexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureIndexBuffer);
            GL.BufferData<Vector4h>(BufferTarget.ArrayBuffer, new IntPtr(textureIndexData.Length * 8), textureIndexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(textureIndexAttrib, 4, VertexAttribPointerType.HalfFloat, true, 0, 0);

            GL.GenBuffers(1, out colorBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, new IntPtr(colorData.Length * 16), colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(colorAttrib, 4, VertexAttribPointerType.Float, true, 0, 0);
            
            GL.GenBuffers(1, out indexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(indexData.Length * 4), indexData, BufferUsageHint.StaticDraw);
        }
        public static void UpdateBuffer()
        {
            
            //basic buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBuffer);
            GL.BufferSubData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(0 * 8), vertexOffset * 8, positionData);

            GL.BindBuffer(BufferTarget.ArrayBuffer, texturePosBuffer);
            GL.BufferSubData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(0 * 8), vertexOffset * 8, texturePosData);

            GL.BindBuffer(BufferTarget.ArrayBuffer, textureIndexBuffer);
            GL.BufferSubData<Vector4h>(BufferTarget.ArrayBuffer, (IntPtr)(0 * 8), (vertexOffset * 4), textureIndexData);

            //color Buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
            GL.BufferSubData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(0 * 16), (vertexOffset * 16), colorData);

            //index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.BufferSubData<int>(BufferTarget.ElementArrayBuffer, (IntPtr)(0 * 4), (indexOffset * 4), indexData);

            vertexOffset = 0;
            indexOffset = 0;
            lastTextureID = -1;
            renderTextureOffset = textureOffset;
            textureOffset = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe static void Render()
        {
            int it = 0;
            int offset = 0;
            int amount = 0;
            GL.Viewport(0, 0, width, height);


            GL.Uniform1(timeUniform, (int)DateTime.Now.Ticks/10000);
            GL.Uniform2(resolutionUniform, new Vector2(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height));

            GL.Uniform1(samplerUniform, 0);

            while (it <= renderTextureOffset)
            {
                amount = textureContinuous[it];
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, textureList[it]);
                //GL.BindTextures()
                //GL.BindTextures(0, 1, textureList);
                GL.DrawElements(BeginMode.Triangles, 3 * amount, DrawElementsType.UnsignedInt, offset * 12 * 1);
                
                offset += amount;
                it++;
            };
        }
    }
}
