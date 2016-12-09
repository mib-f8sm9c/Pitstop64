using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MK64Pitstop.Data.Karts;
using MK64Pitstop.Data;
using System.Drawing;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace ChompShop.Data
{
    public class KartWrapper
    {
        public KartInfo OriginalKart { get; private set; }
        public KartInfo Kart { get; private set; }

        public bool IsModified { get; private set; }
        public bool ComprimisedAnimations { get; private set; }

        //Un-paletted images
        public List<BitmapWrapper> NewImages { get; private set; }

        public KartWrapper(KartWrapper kartWrapper, string newName)
        {
            OriginalKart = new KartInfo(kartWrapper.Kart); //Lose the original kart data
            OriginalKart.KartName = newName;
            CopyFromOriginal();
            NewImages = new List<BitmapWrapper>();
        }

        public KartWrapper(KartInfo origKart)
        {
            OriginalKart = origKart;
            CopyFromOriginal();
            NewImages = new List<BitmapWrapper>();
        }

        private void CopyFromOriginal()
        {
            Kart = new KartInfo(OriginalKart);
        }

        private void CopyToOriginal()
        {
            OriginalKart = new KartInfo(Kart);
        }

        public override string ToString()
        {
            return Kart.KartName;
        }

        public bool ValidKart
        {
            get
            {
                return HasValidNamePlate && HasValidPortraits && HasValidAnimations && HasNonconflictingAnimations && HasImages;
            }
        }
        
        public bool HasValidNamePlate
        {
            get
            {
                return Kart.KartNamePlate != null;
            }
        }

        public bool HasValidPortraits
        {
            get
            {
                return Kart.KartPortraits.Count == 17; //WHATS THE NUMBER?
            }
        }

        public int UniqueAnimsCount
        {
            get
            {
                int uniqueAnims = 0;
                Array flagValues = Enum.GetValues(typeof(KartAnimationSeries.KartAnimationTypeFlag));
                foreach(KartAnimationSeries.KartAnimationTypeFlag flag in flagValues)
                {
                    foreach(KartAnimationSeries anim in Kart.KartAnimations)
                    {
                        if((anim.KartAnimationType & (int)flag) != 0)
                        {
                            uniqueAnims++;
                            break;
                        }
                    }
                }
                return uniqueAnims;
            }
        }

        public bool HasValidAnimations
        {
            get
            {
                return UniqueAnimsCount == 19;
            }
        }

        public bool HasNonconflictingAnimations
        {
            get
            {
                if(Kart == null)
                    return false;

                for (int i = 0; i < Kart.KartAnimations.Count; i++)
                {
                    for (int j = i + 1; j < Kart.KartAnimations.Count; j++)
                    {
                        if (((int)Kart.KartAnimations[i].KartAnimationType & (int)Kart.KartAnimations[j].KartAnimationType) != 0)
                            return false;
                    }
                }

                return true;
            }
        }

        public bool HasImages
        {
            get
            {
                return Kart.KartImages.Images.Count > 0;
            }
        }

        //NOTE: PLEASE USE THESE SETTERS, AS THEY FLIP THE MODIFIED FLAG!
        public void SetName(string newName)
        {
            Kart.KartName = newName;

            IsModified = true;

            ChompShopAlerts.UpdateKartName(this);
        }

        public void SetNamePlate(MK64Image tkmkImage)
        {
            Kart.KartNamePlate = tkmkImage;

            IsModified = true;
        }

        public void AddPortrait(MK64Image image)
        {
            Kart.KartPortraits.Add(image);

            IsModified = true;
        }

        public void RemovePortrait(MK64Image image)
        {
            Kart.KartPortraits.Remove(image);

            IsModified = true;
        }

        public void InsertPortrait(int index, MK64Image image)
        {
            Kart.KartPortraits.Insert(index, image);

            IsModified = true;
        }

        public void SwapPortraits(int index1, int index2)
        {
            MK64Image temp = Kart.KartPortraits[index1];
            Kart.KartPortraits[index1] = Kart.KartPortraits[index2];
            Kart.KartPortraits[index2] = temp;

            IsModified = true;
        }

        public void AddKartImages(List<KartImage> images)
        {
            for(int i = 0; i < images.Count; i++)
            {
                KartImage image = images[i];

                if(Kart.KartImages.Images.ContainsKey(image.Name))
                {
                    images.RemoveAt(i);
                    i--;
                    //NEED TO INCLUDE AN ERROR HERE??
                }
                else
                {
                    Kart.KartImages.Images.Add(image.Name, image);
                }
            }
            if (images.Count > 0)
                IsModified = true;

            //Flip an event?
        }

        public void RemoveKartImage(KartImage image)
        {
            if (Kart.KartImages.Images.ContainsKey(image.Name))
            {
                Kart.KartImages.Images.Remove(image.Name);

                IsModified = true;
            }

            //Flip something to notify that a cart got removed, and 
            // thus update the animation form and whatnot
            ChompShopAlerts.UpdateKartImages(this);
            ComprimisedAnimations = true;
        }

        public void RemoveKartImages(List<KartImage> images)
        {
            foreach (KartImage image in images)
            {
                if (Kart.KartImages.Images.ContainsKey(image.Name))
                {
                    Kart.KartImages.Images.Remove(image.Name);
                    IsModified = true;
                }
            }

            //Remove from the kart images

            //Flip something to notify that a cart got removed, and 
            // thus update the animation form and whatnot
            ChompShopAlerts.UpdateKartImages(this);
            ComprimisedAnimations = true;
        }

        public void SetMainPalette(Palette palette)
        {
            if (Kart.KartImages.ImagePalette == null)
            {
                Kart.KartImages.SetPalette(palette);

                IsModified = true;

                //Events
            }
            //Set the new palette
        }

        public void ClearMainPalette()
        {
            //Convert to new images
            foreach (KartImage image in Kart.KartImages.Images.Values)
                NewImages.Add(new BitmapWrapper(image.Name, image.Images[0].Image));

            Kart.KartImages.ClearPalette();

            Kart.KartImages.Images.Clear();

            IsModified = true;

            //Events
        }

        public void AddNewAnimation(string animName)
        {
            if (Kart.KartAnimations.SingleOrDefault(k => k.Name == animName) != null)
            {
                return; //Name already exists
            }
            KartAnimationSeries anim = new KartAnimationSeries(animName);
            Kart.KartAnimations.Add(anim);

            IsModified = true;

            //Events

        }

        public void RemoveAnimation(KartAnimationSeries anim)
        {
            if (!Kart.KartAnimations.Contains(anim))
                return;

            Kart.KartAnimations.Remove(anim);

            IsModified = true;

            //Events
        }

        public void AddImagetoAnimation(KartAnimationSeries animation, int insertIndex, KartImage image)
        {
            animation.OrderedImageNames.Insert(insertIndex, image.Name);

            IsModified = true;

            //Event
        }

        public void RenameAnimation(KartAnimationSeries animation, string newName)
        {
            animation.Name = newName;

            IsModified = true;

            //Event
        }

        public void RemoveImageFromAnimation(KartAnimationSeries anim, int index)
        {
            if (anim == null || index < 0 || index >= anim.OrderedImageNames.Count)
                return;

            anim.OrderedImageNames.RemoveAt(index);

            IsModified = true;

            //Event
        }

        public void DuplicateImageInAnimation(KartAnimationSeries anim, int index)
        {
            if (anim == null || index < 0 || index >= anim.OrderedImageNames.Count)
                return;

            anim.OrderedImageNames.Insert(index, anim.OrderedImageNames[index]);

            IsModified = true;

            //Event
        }

        public void MoveImageUpInAnimation(KartAnimationSeries anim, int index)
        {
            if (anim == null || index < 1 || index >= anim.OrderedImageNames.Count)
                return;
            
            string indexName = anim.OrderedImageNames[index - 1];
            anim.OrderedImageNames[index - 1] = anim.OrderedImageNames[index];
            anim.OrderedImageNames[index] = indexName;

            IsModified = true;

            //Event
        }

        public void MoveImageDownInAnimation(KartAnimationSeries anim, int index)
        {
            if (anim == null || index < 0 || index >= anim.OrderedImageNames.Count - 1)
                return;

            string indexName = anim.OrderedImageNames[index + 1];
            anim.OrderedImageNames[index + 1] = anim.OrderedImageNames[index];
            anim.OrderedImageNames[index] = indexName;

            IsModified = true;

            //Event
        }

        public void SetAnimationType(KartAnimationSeries anim, int newAnimType)
        {
            if (anim == null)
                return;

            anim.KartAnimationType = newAnimType;

            IsModified = true;

            //Event
        }

        public void UpdateAnimationsWithExistingImages()
        {
            //Bascially remove images from animations where the images are missing
            foreach (KartAnimationSeries anim in Kart.KartAnimations)
            {
                for (int i = 0; i < anim.OrderedImageNames.Count; i++)
                {
                    if (!Kart.KartImages.Images.ContainsKey(anim.OrderedImageNames[i]))
                    {
                        anim.OrderedImageNames.RemoveAt(i);
                        i--;
                    }
                }
            }

            ComprimisedAnimations = false;
            IsModified = true;
        }

        public void RevertChanges()
        {
            CopyFromOriginal();
            IsModified = false;
        }

        public void SaveChanges()
        {
            CopyToOriginal();
            IsModified = false;
        }
    }

    public class BitmapWrapper
    {
        public string Name { get; set; }
        public Bitmap Image { get; set; }

        public BitmapWrapper(string name, Bitmap image)
        {
            Name = name;
            Image = image;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
