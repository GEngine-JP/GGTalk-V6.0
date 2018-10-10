using System;
using System.Collections.Generic;
using System.Text;

namespace GGTalk
{
    /// <summary>
    /// 3DES加密算法。可以直接使用其静态方法进行3DES加/解密。
    /// </summary>
    public class Des3Encryption
    {
        #region Static
        /// <summary>
        /// 使用3DES加密
        /// </summary>        
        public static byte[] Encrypt(byte[] origin, string key)
        {
            Des3Encryption desEncryption = new Des3Encryption(key);
            return desEncryption.Encrypt(origin);
        }

        /// <summary>
        /// 使用3DES解密
        /// </summary> 
        public static byte[] Decrypt(byte[] encrypted, string key)
        {
            Des3Encryption desEncryption = new Des3Encryption(key);
            return desEncryption.Decrypt(encrypted);
        }

        /// <summary>
        /// 使用3DES加密字符串
        /// </summary>  
        public static string EncryptString(string origin, string key)
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(origin);
            byte[] res = Des3Encryption.Encrypt(buff, key);
            return System.Text.Encoding.UTF8.GetString(res);
        }

        /// <summary>
        /// 使用3DES解密字符串
        /// </summary> 
        public static string DecryptString(string encrypted, string key)
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(encrypted);
            byte[] res = Des3Encryption.Decrypt(buff, key);
            return System.Text.Encoding.UTF8.GetString(res);
        }
        #endregion

        #region member and search tables for des algorithms
        //define key Array
        private uint[][] keyArray = new uint[16][];
        //define many table for des algorithms
        #region tables for des encrypt
        private int[] fst_change_tb = {58,50,42,34,26,18,10,2,60,52,44,36,28,20,12,4,
                               62,54,46,38,30,22,14,6,64,56,48,40,32,24,16,8,
                               57,49,41,33,25,17,9,1,59,51,43,35,27,19,11,3,
                               61,53,45,37,29,21,13,5,63,55,47,39,31,23,15,7};
        private int[] lst_change_tb = {40,8,48,16,56,24,64,32, 39,7,47,15,55,23,63,31,
                               38,6,46,14,54,22,62,30, 37,5,45,13,53,21,61,29,
                                36,4,44,12,52,20,60,28, 35,3,43,11,51,19,59,27,
                               34,2,42,10,50,18,58,26, 33,1,41,9,49,17,57,25};
        private int[] mid_change_tb32 = {16,7,20,21, 29,12,28,17, 1,15,23,26,5,18,31,10,
								 2,8,24,14, 32,27,3,9,19,13,30,6, 22,11,4,25};
        private uint[] bits_tb64 = {0x80000000,0x40000000,0x20000000,0x10000000, 0x8000000, 
	                    0x4000000, 0x2000000, 0x1000000, 0x800000, 0x400000,
                        0x200000, 0x100000,  0x80000, 0x40000, 0x20000,0x10000, 
	                    0x8000, 0x4000, 0x2000, 0x1000, 0x800, 0x400, 0x200,
	                    0x100, 0x80,0x40,0x20, 0x10, 0x8, 0x4, 0x2, 0x1,
	                    0x80000000,0x40000000,0x20000000,0x10000000, 0x8000000, 
	                    0x4000000, 0x2000000, 0x1000000, 0x800000, 0x400000,
                        0x200000, 0x100000,  0x80000, 0x40000, 0x20000,0x10000, 
	                    0x8000, 0x4000, 0x2000, 0x1000, 0x800, 0x400, 0x200,
	                    0x100, 0x80,0x40,0x20, 0x10, 0x8, 0x4, 0x2, 0x1};
        private int[] expt_tb = {32,1,2,3,4,5,4,5,6,7,8,9,8,9,10,11,12,13,
                              12,13,14,15,16,17,16,17,18,19,20,21,
                              20,21,22,23,24,25,24,25,26,27,28,29,
                              28,29,30,31,32,1};

        private int[,] SP = new int[,]{
              {
              0xe,0x0,0x4,0xf,0xd,0x7,0x1,0x4,0x2,0xe,0xf,0x2,0xb,
              0xd,0x8,0x1,0x3,0xa,0xa,0x6,0x6,0xc,0xc,0xb,0x5,0x9,
              0x9,0x5,0x0,0x3,0x7,0x8,0x4,0xf,0x1,0xc,0xe,0x8,0x8,
              0x2,0xd,0x4,0x6,0x9,0x2,0x1,0xb,0x7,0xf,0x5,0xc,0xb,
              0x9,0x3,0x7,0xe,0x3,0xa,0xa,0x0,0x5,0x6,0x0,0xd  
              },

             { 
              0xf,0x3,0x1,0xd,0x8,0x4,0xe,0x7,0x6,0xf,0xb,0x2,0x3,
              0x8,0x4,0xf,0x9,0xc,0x7,0x0,0x2,0x1,0xd,0xa,0xc,0x6,
              0x0,0x9,0x5,0xb,0xa,0x5,0x0,0xd,0xe,0x8,0x7,0xa,0xb,
              0x1,0xa,0x3,0x4,0xf,0xd,0x4,0x1,0x2,0x5,0xb,0x8,0x6,
              0xc,0x7,0x6,0xc,0x9,0x0,0x3,0x5,0x2,0xe,0xf,0x9
             },
            { 
              0xa,0xd,0x0,0x7,0x9,0x0,0xe,0x9,0x6,0x3,0x3,0x4,0xf,
              0x6,0x5,0xa,0x1,0x2,0xd,0x8,0xc,0x5,0x7,0xe,0xb,0xc,
              0x4,0xb,0x2,0xf,0x8,0x1,0xd,0x1,0x6,0xa,0x4,0xd,0x9,
              0x0,0x8,0x6,0xf,0x9,0x3,0x8,0x0,0x7,0xb,0x4,0x1,0xf,
              0x2,0xe,0xc,0x3,0x5,0xb,0xa,0x5,0xe,0x2,0x7,0xc                                          
            },
             { 
              0x7,0xd,0xd,0x8,0xe,0xb,0x3,0x5,0x0,0x6,0x6,0xf,0x9,
              0x0,0xa,0x3,0x1,0x4,0x2,0x7,0x8,0x2,0x5,0xc,0xb,0x1,
              0xc,0xa,0x4,0xe,0xf,0x9,0xa,0x3,0x6,0xf,0x9,0x0,0x0,
              0x6,0xc,0xa,0xb,0xa,0x7,0xd,0xd,0x8,0xf,0x9,0x1,0x4,
              0x3,0x5,0xe,0xb,0x5,0xc,0x2,0x7,0x8,0x2,0x4,0xe                         
             },
             { 
              0x2,0xe,0xc,0xb,0x4,0x2,0x1,0xc,0x7,0x4,0xa,0x7,0xb,
              0xd,0x6,0x1,0x8,0x5,0x5,0x0,0x3,0xf,0xf,0xa,0xd,0x3,
              0x0,0x9,0xe,0x8,0x9,0x6,0x4,0xb,0x2,0x8,0x1,0xc,0xb,
              0x7,0xa,0x1,0xd,0xe,0x7,0x2,0x8,0xd,0xf,0x6,0x9,0xf,
              0xc,0x0,0x5,0x9,0x6,0xa,0x3,0x4,0x0,0x5,0xe,0x3
             },
            { 
              0xc,0xa,0x1,0xf,0xa,0x4,0xf,0x2,0x9,0x7,0x2,0xc,0x6,
              0x9,0x8,0x5,0x0,0x6,0xd,0x1,0x3,0xd,0x4,0xe,0xe,0x0,
              0x7,0xb,0x5,0x3,0xb,0x8,0x9,0x4,0xe,0x3,0xf,0x2,0x5,
              0xc,0x2,0x9,0x8,0x5,0xc,0xf,0x3,0xa,0x7,0xb,0x0,0xe,
              0x4,0x1,0xa,0x7,0x1,0x6,0xd,0x0,0xb,0x8,0x6,0xd
            },
            { 
              0x4,0xd,0xb,0x0,0x2,0xb,0xe,0x7,0xf,0x4,0x0,0x9,0x8,
              0x1,0xd,0xa,0x3,0xe,0xc,0x3,0x9,0x5,0x7,0xc,0x5,0x2,
              0xa,0xf,0x6,0x8,0x1,0x6,0x1,0x6,0x4,0xb,0xb,0xd,0xd,
              0x8,0xc,0x1,0x3,0x4,0x7,0xa,0xe,0x7,0xa,0x9,0xf,0x5,
              0x6,0x0,0x8,0xf,0x0,0xe,0x5,0x2,0x9,0x3,0x2,0xc
            },
            { 
              0xd,0x1,0x2,0xf,0x8,0xd,0x4,0x8,0x6,0xa,0xf,0x3,0xb,
              0x7,0x1,0x4,0xa,0xc,0x9,0x5,0x3,0x6,0xe,0xb,0x5,0x0,
              0x0,0xe,0xc,0x9,0x7,0x2,0x7,0x2,0xb,0x1,0x4,0xe,0x1,
              0x7,0x9,0x4,0xc,0xa,0xe,0x8,0x2,0xd,0x0,0xf,0x6,0xc,
              0xa,0x9,0xd,0x0,0xf,0x3,0x3,0x5,0x5,0x6,0x8,0xb
            } 
        };
        #endregion

        #region tables for make keys
        private int[] keyleft = { 57,49,41,33,25,17,9,1,
						 58,50,42,34,26,18,10,2,
						 59,51,43,35,27,19,11,3,60,52,44,36};

        private int[] keyright = {63,55,47,39,31,23,15,7,
						   62,54,46,38,30,22,14,6,
						   61,53,45,37,29,21,13,5,28,20,12,4};
        private int[] lefttable = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        private uint[] leftandtab = { 0x0, 0x80000000, 0xc0000000 };

        private int[] keychoose = {14,17,11,24,1,5,3,28,15,6,21,10,
                                    23,19,12,4,26,8,16,7,27,20,13,2,
                                    41,52,31,37,47,55,30,40,51,45,33,48,
                                    44,49,39,56,34,53,46,42,50,36,29,32};
        #endregion
        #endregion

        #region property
        #region Key
        private string key = "";
        /// <summary>
        /// 密匙
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        #endregion

        #region IniVector
        private string iniVector = "";
        /// <summary>
        /// 如果加密方式是DesCBC模式，iniVector表示初始向量
        /// </summary>
        public string IniVector
        {
            get { return iniVector; }
            set { iniVector = value; }
        }
        #endregion       
        #endregion

        #region Ctor
        public Des3Encryption() { }
        public Des3Encryption(string _key)
        {           
            this.key = _key;
        }
        /// <summary>
        /// Ctor。
        /// </summary>       
        /// <param name="_key">密匙</param>
        /// <param name="_iniVector">如果加密方式是DesCBC模式，_iniVector表示初始向量</param>
        public Des3Encryption(string _key, string _iniVector)
        {           
            this.key = _key;
            this.iniVector = _iniVector;
        }
        #endregion

        #region Encrypt
        //返回的结果的前4个字节为一个int，表示des加密结果的长度，后续字节为加密的真正结果。
        public byte[] Encrypt(byte[] origin)
        {
            int originLen = origin.Length;
            //保证加密的字节长度是8的倍数         
            int len = origin.Length % 8 == 0 ? origin.Length : origin.Length + 8 - origin.Length % 8;
            byte[] bts2 = new byte[len];
            origin.CopyTo(bts2, 0);

            byte[] des_bytes = this.Des3(bts2, key, true);

            byte[] result = new byte[4 + des_bytes.Length];
            byte[] originLenBytes = BitConverter.GetBytes(originLen);
            Buffer.BlockCopy(originLenBytes, 0, result, 0, 4);
            Buffer.BlockCopy(des_bytes, 0, result, 4, des_bytes.Length);
            return result;
        }
        #endregion

        #region Decrypt
        public byte[] Decrypt(byte[] encrypted)
        {
            int originLen = BitConverter.ToInt32(encrypted, 0);
            byte[] encryptedAll = new byte[encrypted.Length - 4];
            Buffer.BlockCopy(encrypted, 4, encryptedAll, 0, encryptedAll.Length);

            byte[] enDes_bytes = this.Des3(encryptedAll, this.key, false);       
            byte[] result = new byte[originLen];
            Buffer.BlockCopy(enDes_bytes, 0, result, 0, originLen);
            return result;
        }
        #endregion

        #region private
        #region Des
        private byte[] Des(byte[] input_data, string key, bool encrypt)// key的长度大于8个字节
        {
            byte[] bts = System.Text.Encoding.UTF8.GetBytes(key);
            uint[] keys = new uint[2];

            keys[0] = BitConverter.ToUInt32(bts, 0);
            keys[1] = BitConverter.ToUInt32(bts, 4);
            byte[] rtn_bytes = this.Des(input_data, keys, encrypt);

            return rtn_bytes;
        }


        private byte[] Des(byte[] data, uint[] key, bool encrypt)//input_data的长度必须是8的倍数
        {
            uint[] in_tmp = new uint[data.Length / 4];
            uint[] out_tmp = new uint[data.Length / 4];
            byte[] rtn_bytes = new byte[data.Length];

            for (int i = 0; i < data.Length; i += 4)
            {
                in_tmp[i / 4] |= data[i]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= data[i + 1]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= data[i + 2]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= data[i + 3];
            }

            out_tmp = this.Des(in_tmp, key, encrypt);

            for (int i = 0; i < out_tmp.Length; i++)
            {
                rtn_bytes[4 * i + 3] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i + 2] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i + 1] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i] |= (byte)(out_tmp[i] & 0xff);
            }

            return rtn_bytes;
        }

        private uint[] Des(uint[] data, uint[] key, bool encrypt)
        {
            uint[] rtn_data = new uint[data.Length];
            uint[] in_dt = new uint[2];
            uint[] out_dt = new uint[2];

            this.DesCreateKeys(key);

            for (int i = 0; i < data.Length; i += 2)
            {
                in_dt[0] = data[i];
                in_dt[1] = data[i + 1];
                out_dt = this.handle_data(in_dt, encrypt);
                rtn_data[i] = out_dt[0];
                rtn_data[i + 1] = out_dt[1];
            }
            return rtn_data;
        }

        private uint[] Des(uint[] data, uint[] key, uint[] iv, bool encrypt)
        {
            uint[] rtn_data = new uint[data.Length];
            uint[] in_dt = new uint[2];
            uint[] out_dt = new uint[2];
            uint cbc_left2 = 0, cbc_right2 = 0;

            this.DesCreateKeys(key);

            for (int i = 0; i < data.Length; i += 2)
            {
                in_dt[0] = data[i];
                in_dt[1] = data[i + 1];
                if (encrypt)
                {
                    in_dt[0] ^= iv[0];
                    in_dt[1] ^= iv[1];
                }
                else
                {
                    cbc_left2 = iv[0];
                    cbc_right2 = iv[1];
                    iv[0] = in_dt[0];
                    iv[1] = in_dt[1];
                }
                out_dt = this.handle_data(in_dt, encrypt);
                if (encrypt)
                {
                    iv[0] = out_dt[0];
                    iv[1] = out_dt[1];
                }
                else
                {
                    out_dt[0] ^= cbc_left2;
                    out_dt[1] ^= cbc_right2;
                }
                rtn_data[i] = out_dt[0];
                rtn_data[i + 1] = out_dt[1];
            }
            return rtn_data;
        }
        #endregion

        #region Des3
        private byte[] Des3(byte[] input_data, string key, bool encrypt)
        {
            for (int i = 0; i < 3; i++)
            {
                input_data = this.Des(input_data, key, encrypt);
            }
            return input_data;
        }
        #endregion

        #region DesTwoKeys
        private byte[] DesTwoKeys(byte[] input_data, string key1, string key2, bool encrypt)
        {

            byte[] des_bytes = this.Des(input_data, key1, encrypt);
            des_bytes = this.Des(des_bytes, key2, !encrypt);
            des_bytes = this.Des(des_bytes, key1, encrypt);

            return des_bytes;
        }
        #endregion

        #region DesCBC
        private byte[] DesCBC(byte[] input_data, string key_str, string iv, bool encrypt)
        {
            byte[] bts = System.Text.Encoding.UTF8.GetBytes(key_str);
            uint[] keys = new uint[2];
            keys[0] = BitConverter.ToUInt32(bts, 0);
            keys[1] = BitConverter.ToUInt32(bts, 4);
            bts = System.Text.Encoding.UTF8.GetBytes(iv);
            uint[] cbc_iv = new uint[2];
            cbc_iv[0] = BitConverter.ToUInt32(bts, 0);
            cbc_iv[1] = BitConverter.ToUInt32(bts, 4);

            uint[] in_tmp = new uint[input_data.Length / 4];
            uint[] out_tmp = new uint[input_data.Length / 4];
            byte[] rtn_bytes = new byte[input_data.Length];

            for (int i = 0; i < input_data.Length; i += 4)
            {
                in_tmp[i / 4] |= input_data[i]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= input_data[i + 1]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= input_data[i + 2]; in_tmp[i / 4] <<= 8;
                in_tmp[i / 4] |= input_data[i + 3];
            }

            out_tmp = this.Des(in_tmp, keys, cbc_iv, encrypt);

            for (int i = 0; i < out_tmp.Length; i++)
            {
                rtn_bytes[4 * i + 3] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i + 2] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i + 1] |= (byte)(out_tmp[i] & 0xff); out_tmp[i] >>= 8;
                rtn_bytes[4 * i] |= (byte)(out_tmp[i] & 0xff);
            }

            return rtn_bytes;

        }
        #endregion

        #region handle_data
        private uint[] handle_data(uint[] data, bool encrypt)
        {
            int i;
            uint tmp;
            uint[] result_data = new uint[2];
            //第一次调整位数
            uint[] newdata = this.change_data(data, this.fst_change_tb);
            if (encrypt)
            {
                for (i = 0; i < 16; i++)
                {
                    make_data(newdata, i);
                }
            }
            else
            {
                for (i = 15; i >= 0; i--)
                {
                    make_data(newdata, i);
                }
            }
            //最后一轮操作不交换左右值
            tmp = newdata[0];
            newdata[0] = newdata[1];
            newdata[1] = tmp;

            //最后一次调整位数
            result_data = change_data(newdata, this.lst_change_tb);

            return result_data;
        }


        #endregion

        #region change_data//数据交换
        private uint[] change_data(uint[] olddata, int[] change_tb)
        {
            uint[] newdata = new uint[2];
            for (int i = 0; i < 64; i++)
            {
                if (i < 32)
                {
                    if (change_tb[i] > 32) // 属于right
                    {
                        if ((olddata[1] & bits_tb64[change_tb[i] - 1]) != 0) newdata[0] |= bits_tb64[i];
                    }
                    else
                    {
                        if ((olddata[0] & bits_tb64[change_tb[i] - 1]) != 0) newdata[0] |= bits_tb64[i];
                    }
                }
                else
                {
                    if (change_tb[i] > 32) // 属于right
                    {
                        if ((olddata[1] & bits_tb64[change_tb[i] - 1]) != 0) newdata[1] |= bits_tb64[i];
                    }
                    else
                    {
                        if ((olddata[0] & bits_tb64[change_tb[i] - 1]) != 0) newdata[1] |= bits_tb64[i];
                    }
                }
            }
            return newdata;
        }
        #endregion

        #region make_data
        private void make_data(uint[] data, int number)
        {
            int j;
            uint[] exp = { 0, 0 };
            byte[] rexpbuf = new byte[8];
            uint datatmp;
            uint oldright = data[1];

            //由32位扩充至48位
            for (j = 0; j < 48; j++)
            {
                //两个32位，每个存放24位
                if (j < 24)
                {
                    if ((data[1] & bits_tb64[expt_tb[j] - 1]) != 0) exp[0] |= (bits_tb64[j] >> 8);
                }
                else
                {
                    if ((data[1] & bits_tb64[expt_tb[j] - 1]) != 0) exp[1] |= bits_tb64[j - 24] >> 8;
                }
            }

            //与密钥异或
            for (j = 0; j < 2; j++)
            {

                exp[j] ^= this.keyArray[number][j] & 0xfffffff;
            }

            //由48位到32位
            rexpbuf[7] = (byte)(exp[1] & 0x3f);
            exp[1] >>= 6;
            rexpbuf[6] = (byte)(exp[1] & 0x3f);
            exp[1] >>= 6;
            rexpbuf[5] = (byte)(exp[1] & 0x3f);
            exp[1] >>= 6;
            rexpbuf[4] = (byte)(exp[1] & 0x3f);

            rexpbuf[3] = (byte)(exp[0] & 0x3f);
            exp[0] >>= 6;
            rexpbuf[2] = (byte)(exp[0] & 0x3f);
            exp[0] >>= 6;
            rexpbuf[1] = (byte)(exp[0] & 0x3f);
            exp[0] >>= 6;
            rexpbuf[0] = (byte)(exp[0] & 0x3f);


            data[1] = 0;
            for (j = 0; j < 7; j++)
            {
                data[1] |= (uint)SP[j, (uint)rexpbuf[j]];
                data[1] <<= 4;
            }
            data[1] |= (uint)SP[j, (uint)rexpbuf[j]];

            //把加密计算后的32位换位
            datatmp = 0;
            for (j = 0; j < 32; j++)
            {
                if ((data[1] & bits_tb64[mid_change_tb32[j] - 1]) != 0) datatmp |= bits_tb64[j];
            }
            data[1] = datatmp;


            //左右相异或, 赋给右，然后原始右值赋给左值
            data[1] ^= data[0];
            data[0] = oldright;
        }
        #endregion

        #region DesCreateKeys
        private void DesCreateKeys(uint[] key) //key是包含两个32位数的数组
        {
            int j;
            uint[] bufkey = { 0, 0 };
            //等分密钥，bufkey包含两个32位的数

            for (j = 0; j < 28; j++)
            {
                if (keyleft[j] > 32)
                {
                    if ((key[1] & bits_tb64[keyleft[j] - 1]) != 0) bufkey[0] |= bits_tb64[j];
                }
                else
                {
                    if ((key[0] & bits_tb64[keyleft[j] - 1]) != 0) bufkey[0] |= bits_tb64[j];
                }

                if (keyright[j] > 32)
                {
                    if ((key[1] & bits_tb64[keyright[j] - 1]) != 0) bufkey[1] |= bits_tb64[j];
                }
                else
                {
                    if ((key[0] & bits_tb64[keyright[j] - 1]) != 0) bufkey[1] |= bits_tb64[j];
                }
            }

            //密钥移位
            for (j = 0; j < 16; j++)
            {
                this.keyArray[j] = make_key(bufkey, j);
            }
        }
        #endregion

        #region make_key
        private uint[] make_key(uint[] in_key, int number)
        {
            uint tmp1, tmp2;
            int j;
            uint[] rtn_key = new uint[2];
            int leftNum = lefttable[number];

            //获取高一位或者高两位
            tmp1 = in_key[0] & leftandtab[leftNum];
            tmp1 = tmp1 >> (28 - leftNum);
            tmp1 &= 0xfffffff0;
            tmp2 = in_key[1] & leftandtab[leftNum];
            tmp2 = tmp2 >> (28 - leftNum);
            tmp2 &= 0xfffffff0;

            //左移
            in_key[0] <<= leftNum;
            in_key[1] <<= leftNum;

            in_key[0] |= tmp1;
            in_key[1] |= tmp2;



            //从56位中选出48位
            for (j = 0; j < 48; j++)
            {
                if (j < 24)
                {
                    if ((in_key[0] & bits_tb64[keychoose[j] - 1]) != 0) rtn_key[0] |= bits_tb64[j];
                }
                else
                {
                    if ((in_key[1] & bits_tb64[keychoose[j] - 29]) != 0) rtn_key[1] |= bits_tb64[j - 24];
                }
            }

            rtn_key[0] >>= 8;
            rtn_key[1] >>= 8;

            return rtn_key;
        }
        #endregion
        #endregion
    }}
