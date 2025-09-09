using UnityEngine;

namespace SBS.ME // Usamos el mismo namespace para que funcione correctamente
{
    // Esta clase hereda toda la funcionalidad de MeshExploder
    public class ReactiveMeshExploder : MeshExploder
    {
        private bool haExplotado = false;

        /// <summary>
        /// Inicia la explosi�n desde un punto de impacto espec�fico en el mundo.
        /// </summary>
        /// <param name="worldPoint">El punto en coordenadas del mundo donde ocurri� el impacto.</param>
        public void EXPLODE_AtPoint(Vector3 worldPoint)
        {
            // Si ya se activ� la explosi�n, no hacemos nada.
            if (haExplotado) return;

            haExplotado = true;

            // Forzamos el modo de explosi�n a "offset" para que use un punto personalizado.
            this.explosionOrigin = ExplosionOrigin.offset;

            // Calculamos el 'offset' necesario. El origen de la explosi�n ser�
            // la posici�n del objeto (su pivote) m�s este vector de desplazamiento.
            // Al hacer que el resultado sea el punto de impacto, la explosi�n
            // se originar� exactamente donde queremos.
            this.ExplosionOffset = worldPoint - this.transform.position;

            // Llamamos a la funci�n EXPLODE() original de la clase base.
            this.EXPLODE();
        }
    }
}