﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public interface IEntityResolver
	{
		public void Initialize(EntityComponentRegistry registry);
	}
}
